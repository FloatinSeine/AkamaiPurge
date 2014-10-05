using System.Configuration;


namespace AkamaiApiProxy.Configuration
{
    public class AkamaiSecurityConfigurationSettings : ConfigurationSection, Logonable, Chunkable
    {
        private readonly static AkamaiSecurityConfigurationSettings _settings = ConfigurationManager.GetSection("akamai") as AkamaiSecurityConfigurationSettings;

        public static AkamaiSecurityConfigurationSettings Settings
        {
            get
            {
                return _settings;
            }
        }

        [ConfigurationProperty("username", IsRequired = true)]
        //[StringValidator(MinLength = 5, MaxLength = 256)]
        public string Username
        {
            get
            {
                return (string)this["username"];
            }
        }

        [ConfigurationProperty("password", IsRequired = true)]
        //[StringValidator(MinLength = 5, MaxLength = 256)]
        public string Password
        {
            get
            {
                return (string)this["password"];
            }
        }

        [ConfigurationProperty("chunkSize", IsRequired = false, DefaultValue = 100)]
        //[StringValidator(MinLength = 5, MaxLength = 256)]
        public int ChunkSize
        {
            get
            {
                return (int)this["chunkSize"];
            }
        }
    }
}
