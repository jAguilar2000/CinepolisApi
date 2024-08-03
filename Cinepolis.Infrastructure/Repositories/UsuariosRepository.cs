using Cinepolis.Core.Entities;
using Cinepolis.Core.Exceptions;
using Cinepolis.Core.Interface;
using Cinepolis.Core.ViewModels;
using Cinepolis.Infrastructure.Data;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinepolis.Infrastructure.Repositories
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly DatabaseContext _context;
        UploadFileRepository upload = new UploadFileRepository();

        public UsuariosRepository(DatabaseContext context) { _context = context; }

        public async Task<IEnumerable<Usuario>> Gets()
        {
            try
            {
                var users = await _context.Usuario.ToListAsync();
                return users;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }

        public async Task<Usuario> Get(int usuarioId)
        {
            try
            {
                var user = await _context.Usuario.FirstOrDefaultAsync(x => x.usuarioId == usuarioId);
                if (user == null)
                    throw new BusinessException("Advertencia, usuario no existe.");

                return user;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }
        public async Task<UsuariosViewModel> GetUserById(int usuarioId)
        {
            try
            {
                Usuario user = await _context.Usuario.FirstOrDefaultAsync(x => x.usuarioId == usuarioId);

                if (user == null)
                    throw new BusinessException("Advertencia, usuario no existe.");

                UsuariosViewModel userData = new UsuariosViewModel
                {
                    usuario = user.usuario,
                    password = user.password,
                    nombres = user.nombres,
                    apellidos = user.apellidos,
                    telefono = user.telefono,
                    correo = user.correo,
                    esAdministrador = false,
                };

               

                return userData;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }

        public async Task<Usuario> Insert(UsuariosViewModel usuario)
        {
            try
            {
                if (usuario == null)
                    throw new BusinessException("Advertencia, favor llenar datos de usuarios.");

                if (usuario.usuario.Contains(" "))
                    throw new BusinessException("Usuario invalido, contiene espacios.");

                var usuarioDB = await _context.Usuario.FirstOrDefaultAsync(x => x.usuario == usuario.usuario);
                if (usuarioDB != null)
                {
                    if (usuarioDB.activo)
                        throw new BusinessException("Usuario ya existe, activo.");
                    else
                        throw new BusinessException("Usuario ya existe, inactivo.");
                }

                Usuario user = new Usuario()
                {
                    usuario = usuario.usuario,
                    password = BCrypt.Net.BCrypt.HashPassword(usuario.password),
                    nombres = usuario.nombres,
                    apellidos = usuario.apellidos,
                    telefono = usuario.telefono,
                    correo = usuario.correo,
                    foto = "",
                    activo = true,
                    verificado = usuario.esAdministrador ? true : false,
                    rolId = usuario.esAdministrador ? 1 : 2

                };
                _context.Usuario.Add(user);

                if (!String.IsNullOrEmpty(usuario.imgBase64))
                {
                    ImagenViewModel img = new ImagenViewModel()
                    {
                        Id = user.usuarioId,
                        ImgBase64 = usuario.imgBase64,
                        Nombre = usuario.usuario
                    };

                    string urlFoto = await UploadFotoBase64(img);
                    user.foto = urlFoto;
                }

                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }

        public async Task<bool> Edit(Usuario usuario)
        {
            try
            {
                var currientUser = await Get(usuario.usuarioId);

                if (currientUser == null)
                    throw new BusinessException("Advertencia, usuario no existe.");

                if (usuario.usuario.Contains(" "))
                    throw new BusinessException("Usuario invalido, contiene espacios.");

                if (currientUser.usuario != usuario.usuario)
                {
                    Usuario? userExist = _context.Usuario.FirstOrDefault(x => x.usuario == usuario.usuario);
                    if (userExist != null)
                    {
                        if (userExist.usuario == usuario.usuario)
                        {
                            if (userExist.activo)
                                throw new BusinessException("Usuario ya existe, activo.");
                            else
                                throw new BusinessException("Usuario ya existe, inactivo.");
                        }
                        currientUser.usuario = usuario.usuario;
                    }
                    else
                    {
                        currientUser.usuario = usuario.usuario;
                    }
                }

                if (!String.IsNullOrEmpty(usuario.imgBase64))
                {
                    ImagenViewModel img = new ImagenViewModel()
                    {
                        Id = currientUser.usuarioId,
                        ImgBase64 = usuario.imgBase64,
                        Nombre = usuario.usuario
                    };

                    string urlFoto = await UploadFotoBase64(img);
                    currientUser.foto = urlFoto;
                }

                currientUser.nombres = usuario.nombres;
                currientUser.apellidos = usuario.apellidos;
                currientUser.telefono = usuario.telefono;
                currientUser.correo = usuario.correo;
                currientUser.activo = usuario.activo;
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
                Usuario? user = await _context.Usuario.FirstOrDefaultAsync(x => x.usuarioId == img.Id);

                if (user == null)
                    throw new BusinessException($"Usuario con id: {img.Id}, no se encontró.");

                Configuraciones? config = await _context.Configuraciones.FirstOrDefaultAsync(x => x.nombre == "DIR_IMG_USUARIOS");

                if (config == null)
                    throw new BusinessException($"No se ha establecido un directorio para Cloudinary.");

                ImageUploadResult cloudImgLoad = upload.UploadFile(img.fileImagen, config.valor);
                user.foto = cloudImgLoad.Url.ToString();
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
                Configuraciones? configuraciones = await _context.Configuraciones.FirstOrDefaultAsync(x => x.nombre == "DIR_IMG_USUARIOS");
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
