using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MyStuff.Clases
{
    public static class Tools
    {
        const string EmailRegex =
        @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
        + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
		[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
        + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
		[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
        + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";

        public static bool ValidarEmail(string email)
        {
            if (email != null)
            {
                return Regex.IsMatch(email, EmailRegex);
            }
            else
            { return false; }
        }

        //private static char g_Gen_DecimalSeparator = Convert.ToChar(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator.ToString());

        public static string DateFormat(DateTime pDate, bool ISO_Format = false)
        {
            string s = string.Empty;

            try
            {
                if (ISO_Format)
                {
                    string yyyy;
                    string mm;
                    string dd;
                    int i_mm = pDate.Month;
                    int i_dd = pDate.Day;

                    if (i_mm < 10)
                    {
                        mm = "0" + i_mm.ToString();
                    }
                    else
                    {
                        mm = i_mm.ToString();
                    }

                    if (i_dd < 10)
                    {
                        dd = "0" + i_dd.ToString();
                    }
                    else
                    {
                        dd = i_dd.ToString();
                    }

                    yyyy = pDate.Year.ToString();

                    s = yyyy + mm + dd;
                }
                else
                {
                    s = string.Format("{0:dd/MMMM/yyyy}", pDate.Date);
                }
            }
            catch (Exception e)
            {
                s = e.ToString();
            }

            return s;
        }

        public static string FormatoHora(DateTime pDate)
        {
            string h;

            h = pDate.ToLongTimeString();

            return h;
        }

    }
}
