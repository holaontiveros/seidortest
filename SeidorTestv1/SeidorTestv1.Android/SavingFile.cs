using System;
using Xamarin.Forms;
using System.IO;
using SeidorTestv1.Droid;
using SeidorTestv1;
using System.Threading;
using System.Threading.Tasks;

[assembly: Dependency(typeof(SavingFile))]
namespace SeidorTestv1
{
    public class SavingFile : ISavingFile
    {
        public async Task Save(string filename, byte[] fileBytes, CancellationToken token)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filePath = Path.Combine(documentsPath, filename);
            using (FileStream fs = File.Create(filePath))
            {
                try
                {
                    fs.Seek(0, SeekOrigin.End);
                    await fs.WriteAsync(fileBytes, 0, fileBytes.Length, token);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
                
            }

        }
    }
}
