

using ProgramPartListWeb.Areas.Circuit.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Circuit.Interface
{
    public interface IComponents
    {
        Task<List<PartlistComponents>> GetComponentsParts(string plan);
        Task<bool> UploadComponentParts(List<PartlistComponents> plan);
    }
}
