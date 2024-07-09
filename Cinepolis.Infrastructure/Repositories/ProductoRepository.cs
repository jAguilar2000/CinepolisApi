using Cinepolis.Core.Entities;
using Cinepolis.Core.Exceptions;
using Cinepolis.Core.Interface;
using Cinepolis.Core.ViewModels;
using Cinepolis.Infrastructure.Data;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;

namespace Cinepolis.Infrastructure.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly DatabaseContext _context;
        UploadFileRepository upload = new UploadFileRepository();
        public ProductoRepository(DatabaseContext context) { _context = context; }

        public async Task<IEnumerable<Producto>> Gets()
        {
            try
            {
                var result = await _context.Producto.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }
        public async Task<Producto> Get(int productoId)
        {
            try
            {
                var result = await _context.Producto.FirstOrDefaultAsync(x => x.productoId == productoId);
                if (result == null)
                    throw new BusinessException("Advertencia, Producto no existe.");

                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }
        public async Task Insert(Producto producto)
        {
            try
            {
                if (producto == null)
                    throw new BusinessException("Advertencia, favor llenar datos de la pelicula.");

                if (String.IsNullOrEmpty(producto.descripcion))
                    throw new BusinessException("Favor llenar campo Descripción.");

                if (producto.precio <= 0)
                    throw new BusinessException("Precio Invalido.");

                var prodDB = await _context.Producto.FirstOrDefaultAsync(x => x.descripcion == producto.descripcion);

                if (prodDB != null)
                {
                    if (prodDB.activo)
                        throw new BusinessException("Producto ya existe, activo.");
                    else
                        throw new BusinessException("Producto ya existe, inactivo.");
                }

                producto.activo = true;
                Producto newProducto = new Producto();
                newProducto = producto;
                _context.Producto.Add(newProducto);

                if (!String.IsNullOrEmpty(producto.imgBase64))
                {
                    ImagenViewModel img = new ImagenViewModel()
                    {
                        Id = newProducto.productoId,
                        ImgBase64 = producto.imgBase64,
                        Nombre = producto.descripcion
                    };

                    string urlFoto = await UploadFotoBase64(img);
                    newProducto.foto = urlFoto;
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }
        public async Task<bool> Edit(Producto producto)
        {
            try
            {
                var currientProd = await Get(producto.productoId);

                if (currientProd == null)
                    throw new BusinessException("Advertencia, Producto no existe.");

                if (String.IsNullOrEmpty(currientProd.descripcion))
                    throw new BusinessException("Favor llenar campo Descripción.");

                if (producto.precio <= 0)
                    throw new BusinessException("Precio inavalido.");

                if (!String.IsNullOrEmpty(producto.imgBase64))
                {
                    ImagenViewModel img = new ImagenViewModel()
                    {
                        Id = currientProd.productoId,
                        ImgBase64 = producto.imgBase64,
                        Nombre = producto.descripcion
                    };

                    string urlFoto = await UploadFotoBase64(img);
                    currientProd.foto = urlFoto;
                }

                currientProd.activo = producto.activo;
                int row = await _context.SaveChangesAsync();
                return row > 0;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}");
            }
        }
        public async Task<bool> UploadImage(ImagenFileViewModel img)
        {
            try
            {
                Producto? producto = await _context.Producto.FirstOrDefaultAsync(x => x.productoId == img.Id);

                if (producto == null)
                    throw new BusinessException($"Producto con id: {img.Id}, no se encontró.");

                Configuraciones? config = await _context.Configuraciones.FirstOrDefaultAsync(x => x.nombre == "DIR_IMG_PRODUCTO");

                if (config == null)
                    throw new BusinessException($"No se ha establecido un directorio para Cloudinary.");

                ImageUploadResult cloudImgLoad = upload.UploadFile(img.fileImagen, config.valor);
                producto.foto = cloudImgLoad.Url.ToString();
                int row = await _context.SaveChangesAsync();
                return row > 0;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}");
            }
        }
        public async Task<string> UploadFotoBase64(ImagenViewModel foto)
        {
            try
            {
                Configuraciones? configuraciones = await _context.Configuraciones.FirstOrDefaultAsync(x => x.nombre == "DIR_IMG_PRODUCTO");
                string dir = configuraciones?.valor ?? "";

                ImageUploadResult cloudImgLoad = upload.UploadBase64(foto, dir);
                string url = cloudImgLoad.Url.ToString();
                return url;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}");
            }
        }
    }
}
