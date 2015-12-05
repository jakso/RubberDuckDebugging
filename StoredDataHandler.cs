using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace Rubber_Duck_Debugging
{
    public static class StoredDataHandler
    {
        public static async Task<StoredData> GetData()
        {
            var folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var file = await folder.GetFileAsync("stored.dat");
            var lines = await FileIO.ReadLinesAsync(file);

            var noOfProblems = lines[0].Split(';')[0];
            var totalTime = lines[0].Split(';')[1];

            return new StoredData { NumberOfProblems = noOfProblems, TotalTimeUsed = totalTime};
        }

        public static async void WriteData(string number, string time)
        {
            var folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var file = await folder.GetFileAsync("stored.dat");
            await FileIO.WriteTextAsync(file, number + ";" + time);
        }
    }
}
