using Microsoft.Web.XmlTransform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTransform
{
    public class Program
    {
        public static int Main(params string[] args)
        {
            var sourceParm = args.FirstOrDefault(x => x.StartsWith("-src", StringComparison.OrdinalIgnoreCase));
            var transformParm = args.FirstOrDefault(x => x.StartsWith("-transform", StringComparison.OrdinalIgnoreCase) || x.StartsWith("-trans", StringComparison.OrdinalIgnoreCase));
            var destParm = args.FirstOrDefault(x => x.StartsWith("-dest", StringComparison.OrdinalIgnoreCase));

            try
            {
                ValidateParm(sourceParm, nameof(sourceParm));

                var sourceFile = GetOptionValue(sourceParm);
                var transFile = GetOptionValue(transformParm);
                var destFile = GetOptionValue(destParm);

                Console.WriteLine($"start application config transform");
                var originalFileXml = new System.Xml.XmlDocument();
                originalFileXml.Load(sourceFile);

                using (var xmlTransform = new XmlTransformation(transFile))
                {
                    if (xmlTransform.Apply(originalFileXml) == false)
                        throw new Exception("Configuration file transform failure, please confirm the transform file");

                    // originalFileXml is now transformed
                    originalFileXml.Save(destFile);
                    Console.WriteLine("Transform success");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        static void ValidateParm(string param, string paramName)
        {
            if (string.IsNullOrWhiteSpace(param))
            {
                throw new Exception($"{paramName} parameter can't be null");
            }

            if (param.IndexOf("=") < 0 && param.LastIndexOf("=") != param.IndexOf("="))
            {
                throw new Exception($"invalid format for parameter {paramName}, it should be -parameterName=parameterValue");
            }
        }

        static string GetOptionValue(string param)
        {
            return param.Split('=')[1];
        }
    }
}
