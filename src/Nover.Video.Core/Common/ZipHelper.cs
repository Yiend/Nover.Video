﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Nover.Video.Core
{
    /// <summary>
    /// 提供一些操作ZIP文件的工具方法
    /// </summary>
    public static class ZipHelper
    {
        /// <summary>
        /// 将ZIP文件解压缩到指定的目录
        /// </summary>
        /// <param name="zipPath">需要解压缩的ZIP文件</param>
        /// <param name="extractPath">要释放的目录</param>
        public static void ExtractFiles(string zipPath, string extractPath)
        {
            if (string.IsNullOrEmpty(zipPath))
                throw new ArgumentNullException("zipPath");
            if (string.IsNullOrEmpty(extractPath))
                throw new ArgumentNullException("extractPath");

            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {

                    // 忽略目录
                    if (entry.FullName.EndsWith("/"))
                    {
                        string path = Path.Combine(extractPath, entry.FullName.TrimEnd('/'));
                        if (Directory.Exists(path) == false)
                            Directory.CreateDirectory(path);

                        continue;
                    }

                    // 计算要释放到哪里
                    string targetFile = Path.Combine(extractPath, entry.FullName);

                    // 如果要释放的目标目录不存在，就创建目标
                    string destPath = Path.GetDirectoryName(targetFile);
                    Directory.CreateDirectory(destPath);

                    // 如果目标文件已经存在，就删除
                    RetryFile.Delete(targetFile);

                    // 释放文件
                    entry.ExtractToFile(targetFile, true);
                }
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destinationFileName"></param>
        /// <param name="overwrite"></param>
        /// <returns></returns>
        public static Stream ExtractToStream(this ZipArchiveEntry source, string destinationFileName, bool overwrite)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destinationFileName == null)
            {
                throw new ArgumentNullException("destinationFileName");
            }
            FileMode mode = !overwrite ? FileMode.CreateNew : FileMode.Create;

            return File.Open(destinationFileName, mode, FileAccess.Write, FileShare.None);
        }


        /// <summary>
        /// 读取ZIP到内存字典中
        /// </summary>
        /// <param name="zipPath">需要读取的ZIP文件</param>
        /// <returns></returns>
        public static List<Tuple<string, byte[]>> Read(string zipPath)
        {
            if (string.IsNullOrEmpty(zipPath))
                throw new ArgumentNullException("zipPath");

            List<Tuple<string, byte[]>> result = new List<Tuple<string, byte[]>>();


            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {

                    // 目录
                    if (entry.FullName.EndsWith("/"))
                        continue;


                    // 读取某个文件内容
                    // 由于读取过程涉及解压缩，所以不能一次到 byte[]，只好引入一个内存流
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (Stream steam = entry.Open())
                        {
                            steam.CopyTo(ms);
                        }
                        ms.Position = 0;

                        Tuple<string, byte[]> tuple = new Tuple<string, byte[]>(entry.FullName, ms.ToArray());
                        result.Add(tuple);
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// 将指定的目录打包成ZIP文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="zipPath"></param>
        public static void Compress(string path, string zipPath)
        {
            RetryFile.Delete(zipPath);

            // 直接调用 .NET framework
            ZipFile.CreateFromDirectory(path, zipPath);
        }



        /// <summary>
        /// 根据指定的包内文件名及对应的文件内容打包成ZIP文件
        /// </summary>
        /// <param name="files"></param>
        /// <param name="zipPath"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202")]
        public static void Compress(List<Tuple<string, byte[]>> files, string zipPath)
        {
            if (files == null)
                throw new ArgumentNullException(nameof(files));
            if (string.IsNullOrEmpty(zipPath))
                throw new ArgumentNullException(nameof(zipPath));


            RetryFile.Delete(zipPath);


            using (FileStream file = RetryFile.Create(zipPath))
            {
                using (ZipArchive zip = new ZipArchive(file, ZipArchiveMode.Create, true, Encoding.UTF8))
                {

                    foreach (Tuple<string, byte[]> tuple in files)
                    {

                        var entry = zip.CreateEntry(tuple.Item1, CompressionLevel.Optimal);

                        using (BinaryWriter writer = new BinaryWriter(entry.Open()))
                        {
                            writer.Write(tuple.Item2);
                        }
                    }
                }
            }
        }



        /// <summary>
        /// 根据指定的包内文件名及对应的文件清单打包成ZIP文件
        /// </summary>
        /// <param name="files">要打包的文件清单，可以为string（指示文件路径）或者byte[]（文件内容）</param>
        /// <param name="zipPath">zip文件的保存路径</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202")]
        public static void Compress(List<Tuple<string, object>> files, string zipPath)
        {
            if (files == null)
                throw new ArgumentNullException(nameof(files));
            if (string.IsNullOrEmpty(zipPath))
                throw new ArgumentNullException(nameof(zipPath));


            RetryFile.Delete(zipPath);


            using (FileStream file = RetryFile.Create(zipPath))
            {
                using (ZipArchive zip = new ZipArchive(file, ZipArchiveMode.Create, true, Encoding.UTF8))
                {

                    foreach (Tuple<string, object> tuple in files)
                    {

                        var entry = zip.CreateEntry(tuple.Item1, CompressionLevel.Optimal);

                        Type dataType = tuple.Item2.GetType();

                        if (dataType == typeof(string))
                        {
                            using (var stream = entry.Open())
                            {
                                using (FileStream fs = RetryFile.OpenRead((string)tuple.Item2))
                                {
                                    fs.CopyTo(stream);
                                }
                            }
                        }
                        else if (dataType == typeof(byte[]))
                        {
                            using (BinaryWriter writer = new BinaryWriter(entry.Open()))
                            {
                                writer.Write((byte[])tuple.Item2);
                            }
                        }
                        else
                        {
                            // 暂且忽略错误的参数吧。
                        }
                    }
                }
            }
        }




    }
}
