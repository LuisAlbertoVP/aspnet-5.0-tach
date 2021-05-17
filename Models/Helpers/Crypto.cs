using System;
using System.Text;
using System.Security.Cryptography;
using Tach.Models.Entities;

namespace Tach.Models.Helpers {
    public class Crypto {

        public static string HashPassword(string clave) {
            var combinedPassword = string.Concat(clave, "S3creT-@TaC4_2o20:LV+1");
            var bytes = Encoding.UTF8.GetBytes(combinedPassword);
            var hash = new SHA256Managed().ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public static Usuario HashPassword(Usuario usuario) {
            usuario.Clave = HashPassword(usuario.Clave);
            return usuario;
        }
    }
}