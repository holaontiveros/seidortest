using System;
using Xamarin.Forms;
using System.IO;
using SeidorTestv1.iOS;
using System.Threading;
using System.Threading.Tasks;

[assembly: Dependency(typeof(SavingFile))]
namespace SeidorTestv1.iOS
{
    public class SavingFile : ISavingFile
    {
        public async Task Save(string filename, byte[] fileBytes, CancellationToken token)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filePath = Path.Combine(documentsPath, filename);
            FileStream fs = File.OpenWrite(filePath);
            await fs.WriteAsync(fileBytes, 0, fileBytes.Length, token);
        }
    }
}
