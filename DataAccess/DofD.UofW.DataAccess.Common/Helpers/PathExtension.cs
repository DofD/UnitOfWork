using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DofD.UofW.DataAccess.Common.Helpers
{
    /// <summary>
    ///     Расширение класса Path
    /// </summary>
    public static class PathExtension
    {
        /// <summary>
        ///     Текущий путь сборки
        /// </summary>
        public static string AssemblyDirectory
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly();

                return GetAssemblyDirectory(assembly);
            }
        }

        /// <summary>
        ///     Получить все сборки из текущей директории
        /// </summary>
        /// <returns>Все сборки из текущей директории</returns>
        public static IEnumerable<Assembly> GetAssemblyCurrentDirectory()
        {
            return GetAssembly(AssemblyDirectory);
        }

        /// <summary>
        ///     Получить путь к сборки
        /// </summary>
        /// <param name="assembly">Сборка</param>
        /// <returns>Полный путь</returns>
        public static string GetAssemblyDirectory(Assembly assembly)
        {
            var codeBase = assembly.CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);

            return Path.GetDirectoryName(path);
        }

        /// <summary>
        ///     Получить все сборки из директории
        /// </summary>
        /// <param name="path">Путь до директории</param>
        /// <returns>Набор сборок</returns>
        public static IEnumerable<Assembly> GetAssembly(string path)
        {
            return Directory.GetFiles(path, "*.dll").Select(Assembly.LoadFile).ToArray();
        }
    }
}