using Cinepolis.Core.Entities;
using Cinepolis.Core.Exceptions;
using Cinepolis.Core.Interface;
using Cinepolis.Core.ViewModels;
using Cinepolis.Infrastructure.Data;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;

namespace Cinepolis.Infrastructure.Repositories
{
    public class CinesRepository : ICinesRepository
    {
        private readonly DatabaseContext _context;
        UploadFileRepository upload = new UploadFileRepository();
        public CinesRepository(DatabaseContext context) { _context = context; }

        public async Task<IEnumerable<Cines>> Gets()
        {
            try
            {
                var result = await _context.Cines.AsNoTracking().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }
        public async Task<Cines> Get(int cineId)
        {
            try
            {
                var result = await _context.Cines.FirstOrDefaultAsync(x => x.cineId == cineId);
                if (result == null)
                    throw new BusinessException("Advertencia, Cine no existe.");

                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }

        public async Task Insert(Cines cine)
        {
            try
            {
                if (cine == null)
                    throw new BusinessException("Advertencia, favor llenar datos de la cine.");

                if (String.IsNullOrEmpty(cine.descripcion))
                    throw new BusinessException("Favor llenar campo Descripción.");

                var cineDB = await _context.Cines.FirstOrDefaultAsync(x => x.descripcion == cine.descripcion);

                if (cineDB != null)
                {
                    if (cineDB.activo)
                        throw new BusinessException("Cine ya existe, activo.");
                    else
                        throw new BusinessException("Cine ya existe, inactivo.");
                }

                cine.activo = true;
                Cines newCine = new Cines();
                newCine = cine;
                _context.Cines.Add(newCine);

                if (!String.IsNullOrEmpty(cine.imgBase64))
                {
                    ImagenViewModel img = new ImagenViewModel()
                    {
                        Id = newCine.cineId,
                        ImgBase64 = cine.imgBase64,
                        Nombre = cine.descripcion
                    };

                    string urlFoto = await UploadFotoBase64(img);
                    newCine.foto = urlFoto;
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }

        public async Task<bool> Edit(Cines cine)
        {
            try
            {
                var currientCine = await Get(cine.cineId);

                if (currientCine == null)
                    throw new BusinessException("Advertencia, Pelicula no existe.");
                if (String.IsNullOrEmpty(cine.descripcion))
                    throw new BusinessException("Favor llenar campo Descripción.");

                if (!String.IsNullOrEmpty(cine.imgBase64))
                {
                    ImagenViewModel img = new ImagenViewModel()
                    {
                        Id = currientCine.cineId,
                        ImgBase64 = cine.imgBase64,
                        Nombre = cine.descripcion
                    };

                    string urlFoto = await UploadFotoBase64(img);
                    currientCine.foto = urlFoto;
                }

                currientCine.activo = cine.activo;
                currientCine.descripcion = cine.descripcion;
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
                Cines? cine = await _context.Cines.FirstOrDefaultAsync(x => x.cineId == img.Id);

                if (cine == null)
                    throw new BusinessException($"Cine con id: {img.Id}, no se encontró.");

                Configuraciones? config = await _context.Configuraciones.FirstOrDefaultAsync(x => x.nombre == "DIR_IMG_CINE");

                if (config == null)
                    throw new BusinessException($"No se ha establecido un directorio para Cloudinary.");

                ImageUploadResult cloudImgLoad = upload.UploadFile(img.fileImagen, config.valor);
                cine.foto = cloudImgLoad.Url.ToString();
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
                Configuraciones? configuraciones = await _context.Configuraciones.FirstOrDefaultAsync(x => x.nombre == "DIR_IMG_CINE");
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
