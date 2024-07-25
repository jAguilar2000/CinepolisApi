using Cinepolis.Core.Entities;
using Cinepolis.Core.Exceptions;
using Cinepolis.Core.Interface;
using Cinepolis.Core.ViewModels;
using Cinepolis.Infrastructure.Data;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;

namespace Cinepolis.Infrastructure.Repositories
{
    public class PeliculaRepository : IPeliculaRepository
    {
        private readonly DatabaseContext _context;
        UploadFileRepository upload = new UploadFileRepository();
        public PeliculaRepository(DatabaseContext context) { _context = context; }

        public async Task<IEnumerable<Pelicula>> Gets()
        {
            try
            {
                var result = await _context.Pelicula.Include(x=> x.Genero).Include(x=> x.TipoPelicula).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }
        public async Task<Pelicula> Get(int peliculaId)
        {
            try
            {
                var result = await _context.Pelicula.FirstOrDefaultAsync(x => x.peliculaId == peliculaId);
                if (result == null)
                    throw new BusinessException("Advertencia, Pelicula no existe.");

                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }

        public async Task Insert(Pelicula pelicula)
        {
            try
            {
                if (pelicula == null)
                    throw new BusinessException("Advertencia, favor llenar datos de la pelicula.");

                if (String.IsNullOrEmpty(pelicula.titulo))
                    throw new BusinessException("Favor llenar campo Titulo.");

                if (pelicula.generoId == 0)
                    throw new BusinessException("Favor llenar campo Genero.");

                if (pelicula.tipoPeliculaId == 0)
                    throw new BusinessException("Favor llenar campo Tipo Pelicula.");

                if (String.IsNullOrEmpty(pelicula.sinopsis))
                    throw new BusinessException("Favor llenar campo Sinopsis.");

                //if (String.IsNullOrEmpty(pelicula.foto))
                //    throw new BusinessException("Falta la fotografia.");

                var peliDB = await _context.Pelicula.FirstOrDefaultAsync(x => x.titulo == pelicula.titulo);

                if (peliDB != null)
                {
                    if (peliDB.activo)
                        throw new BusinessException("Pelicula ya existe, activo.");
                    else
                        throw new BusinessException("Pelicula ya existe, inactivo.");
                }

                pelicula.activo = true;
                Pelicula newPelicula = new Pelicula();
                newPelicula = pelicula;
              

                if (!String.IsNullOrEmpty(pelicula.imgBase64))
                {
                    ImagenViewModel img = new ImagenViewModel()
                    {
                        Id = newPelicula.peliculaId,
                        ImgBase64 = pelicula.imgBase64,
                        Nombre = pelicula.titulo
                    };

                    string urlFoto = await UploadFotoBase64(img);
                    newPelicula.foto = urlFoto;
                }
                newPelicula.Genero = null;
                _context.Pelicula.Add(newPelicula);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }

        public async Task<bool> Edit(Pelicula pelicula)
        {
            try
            {
                var currientPeli = await Get(pelicula.peliculaId);

                if (currientPeli == null)
                    throw new BusinessException("Advertencia, Pelicula no existe.");
                if (String.IsNullOrEmpty(pelicula.titulo))
                    throw new BusinessException("Favor llenar campo Titulo.");

                if (pelicula.generoId == 0)
                    throw new BusinessException("Favor llenar campo Genero.");

                if (String.IsNullOrEmpty(pelicula.sinopsis))
                    throw new BusinessException("Favor llenar campo Sinopsis.");

                //if (String.IsNullOrEmpty(pelicula.foto))
                //    throw new BusinessException("Falta la fotografia.");

                if (!String.IsNullOrEmpty(pelicula.imgBase64))
                {
                    ImagenViewModel img = new ImagenViewModel()
                    {
                        Id = currientPeli.peliculaId,
                        ImgBase64 = pelicula.imgBase64,
                        Nombre = pelicula.titulo
                    };

                    string urlFoto = await UploadFotoBase64(img);
                    currientPeli.foto = urlFoto;
                }

                currientPeli.activo = pelicula.activo;
                currientPeli.sinopsis = pelicula.sinopsis;
                currientPeli.titulo= pelicula.titulo;
                currientPeli.hora = pelicula.hora;
                currientPeli.minutos = pelicula.minutos;
                currientPeli.tipoPeliculaId = pelicula.tipoPeliculaId;
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
                Pelicula? pelicula = await _context.Pelicula.FirstOrDefaultAsync(x => x.peliculaId == img.Id);

                if (pelicula == null)
                    throw new BusinessException($"Pelicula con id: {img.Id}, no se encontró.");

                Configuraciones? config = await _context.Configuraciones.FirstOrDefaultAsync(x => x.nombre == "DIR_IMG_PELICULA");

                if (config == null)
                    throw new BusinessException($"No se ha establecido un directorio para Cloudinary.");

                ImageUploadResult cloudImgLoad = upload.UploadFile(img.fileImagen, config.valor);
                pelicula.foto = cloudImgLoad.Url.ToString();
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
                Configuraciones? configuraciones = await _context.Configuraciones.FirstOrDefaultAsync(x => x.nombre == "DIR_IMG_PELICULA");
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