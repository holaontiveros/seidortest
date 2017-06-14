using System;
using System.Threading;
using System.Threading.Tasks;

namespace SeidorTestv1
{
    public interface ISavingFile
    {
        Task Save(string path, byte[] fileBytes, CancellationToken token);
    }
}