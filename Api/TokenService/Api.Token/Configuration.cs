using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Api.Token
{
    /// <summary>
    /// Api configuration class
    /// </summary>
    public class Configuration
    {
 
        public JWTConfig JWTConfig { get; set; }
    }

    /// <summary>
    /// JWT Configuration settings
    /// </summary>
    public class JWTConfig
    {
        public int ExpireMinutes { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }        
        public string Key { get; set; }
    }

}
