using System;
using System.Collections.Generic;
using System.Text;
using BE;
using DAL;

namespace BLL
{
    public class EvaluacionBLL
    {
        TratamientoBLL gestorTratamiento = new TratamientoBLL();
        PacienteBLL gestorPaciente = new PacienteBLL();

        public List<EvaluacionBE> Listar()
        {
            EvaluacionMapper m = new EvaluacionMapper();
            return m.Listar();
        }
    }
}
