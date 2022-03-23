using System;
using System.Collections.Generic;
using BGAPI.Entities;

namespace BGAPI.Workers
{
    internal sealed class FileGetterByName : IFileGetter
    {

        private static readonly Dictionary<string, string> Dictionary = new() {
            {"list.md5","https://disk.skbkontur.ru/index.php/s/m66mPzsC3iB2Ear/download"},
            {"ver.id","https://disk.skbkontur.ru/index.php/s/d6qRCoTf4rxicFf/download"},
            {"7z.exe","https://disk.skbkontur.ru/index.php/s/aookpz3wXMBNqiX/download"},
            {"listfile.txt","https://disk.skbkontur.ru/index.php/s/q9eHySqPBjRk7aF/download"},
            {"ver_release.id","https://disk.skbkontur.ru/index.php/s/ZE75rxJW7PTexeJ/download"},
            {"list_release.md5","https://disk.skbkontur.ru/index.php/s/RXiqf9xxMmb5ZP3/download"}
        };

        private readonly IDownloader _downloader;
        private readonly string _fileName;

        public FileGetterByName(IDownloader downloader, string fileName) => (_downloader, _fileName) = (downloader, fileName);


        public FileData Get() => _downloader.Download(GetUrl(_fileName));

        private static string GetUrl(string fileName) {
            if (Dictionary.ContainsKey(fileName)) {
                return Dictionary[fileName];
            }

            throw new ArgumentOutOfRangeException($"{fileName} notfound");
        }
    }
}