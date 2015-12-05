using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace Rubber_Duck_Debugging
{
    public static class StoredDataHandler
    {
        public static async Task<List<StoredData>> GetData()
        {
            var folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var file = await folder.GetFileAsync("stored.dat");
            var lines = await FileIO.ReadLinesAsync(file);

            return (from line in lines
                    where line.StartsWith("NumberOfProblems:")
                    select new StoredData { NumberOfProblems = (line.Split(':'))[1] }).ToList();
        }

        public static async void WriteData(string number)
        {
            var folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var file = await folder.GetFileAsync("stored.dat");
            await FileIO.WriteTextAsync(file, "NumberOfProblems:" + number);
        }
    }
}
