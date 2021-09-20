using FabricProject.Dto.Color;
using FabricProject.Models.Models;
using FabricProject.SharedKernal.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace FabrciProject.IData.Interfaces
{
    public interface IColorRepository
    {
        IEnumerable<ColorDto> GetColors(string query = null);
        ColorDto SetColor(ColorDto colorDto);
        ColorDto GetById(int id);
        OperationResult<int> RemoveColor(int id);

        bool IsColorExit(string colorName);


    }
}
