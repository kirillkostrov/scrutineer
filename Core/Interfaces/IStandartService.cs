using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IStandartService
    {
        IEnumerable<NamedItem> GetStandartsList();
    }
}