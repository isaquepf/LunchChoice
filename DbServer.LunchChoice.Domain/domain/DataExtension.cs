using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DbServer.LunchChoice.Core.domain
{
    public static class DataExtension
    {
        public static int GetWeekOfYear(this DateTime data)
        {
            var ptBR = new CultureInfo("pt-BR");
            Calendar calendar = ptBR.Calendar;

            CalendarWeekRule regraSemana = ptBR.DateTimeFormat.CalendarWeekRule;
            return calendar.GetWeekOfYear(DateTime.Now, regraSemana, DayOfWeek.Sunday);            
        }
    }
}
