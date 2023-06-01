using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class LoggerDemo
    {
        private readonly ILogger _logger;

        public LoggerDemo(ILogger<LoggerDemo> logger)
        {
            _logger = logger;
        }

        public void Work()
        {
            try
            {
                // _logger.LogTrace("Work begin");

                using (var scope1 = _logger.BeginScope("Work"))
                using (var scope2 = _logger.BeginScope("Ala ma kota"))
                using (var scope5 = _logger.BeginScope("my format {0} ...", GetType().Name))
                using (var scope6 = _logger.BeginScope(new Dictionary<string, string>() { { "a", "aa" }, { "b", "bb" } }))
                {

                    for (int i = 0; i < 10; i++)
                    {
                        using (var scope3 = _logger.BeginScope(i.ToString()))
                        using (var scope4 = _logger.BeginScope((i * i).ToString()))
                        {
                            try
                            {
                                //_logger.LogInformation($"Inner {i}");

                                if (i == 5)
                                {
                                    throw new IndexOutOfRangeException();
                                }
                            }
                            catch (Exception e)
                            {
                                _logger.LogError(e, "Oops.. błąd...");
                            }

                            if (i == 9)
                                throw new Exception();
                        }
                    }
                }

                //_logger.LogTrace("Work end");
            }
            catch(Exception e) when (LogError(e))
            {
            }
        }

        private bool LogError(Exception e)
        {
            _logger.LogError(e, "Bad thing...");
            return true;
        }
    }
}
