using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobLink.WebLeilao.Web.Models
{
    public class NuloOuVazio : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return true;
            else
                return false;
        }
    }
}