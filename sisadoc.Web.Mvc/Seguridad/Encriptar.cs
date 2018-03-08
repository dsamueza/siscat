using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UTE.Encriptacion;


namespace sisadoc.Web.Mvc.Seguridad
{
   public class Encriptar
    {
   public string encriptartexto (string enc)
    {
    
    return encriptador.encryptQueryString(enc);
     
       
    }
       
   public string desencriptartexto(string enc)
   {

     return encriptador.decryptQueryString(enc);
      


   }
    }
}
