using aYoUnitConversion.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aYoUnitConversion.Controllers
{
    /// <summary>
    /// ImperialToMetricConverter Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ImperialToMetricConverterController : ControllerBase
    {
        protected readonly UnitConversionContext _dataContext;
        protected ILogger<UnitConversionContext> _log;

        public ImperialToMetricConverterController(UnitConversionContext dataContext,
            ILogger<UnitConversionContext> log)
        {
            _dataContext = dataContext;
            _log = log;
        }

        /// <summary>
        /// Get Method to retrive all available conversion factors
        /// </summary>
        /// <returns>A method will return all available conversion factors. </returns>
        [HttpGet()]
        public IActionResult Get()
        {

            var data = _dataContext.UnitConversionFactors.ToList();

            return Ok(new Response()
            {
                IsSuccess = true,
                Message = "All available Unit Conversions are fetched " +
                "with its conversion Factor, -1 factor indicates that they are " +
                "integrated in solution which could not altered from database.",
                ResponseData = data
            });
        }

        [HttpPost]
        public IActionResult Post(ConversionInput conversionInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong Input Data.");
            }

            ConversionResult result = Convert(conversionInput);
            if (result == null)
            {
                return Ok(new Response()
                {
                    IsSuccess = true,
                    Message = "Converstion factor not found for input units.",
                    ResponseData = conversionInput
                });
            }
            else
            {

                return Ok(new Response()
                {
                    IsSuccess = true,
                    Message = "Value converted successfully.",
                    ResponseData = result
                });
            }
        }

        /// <summary>
        /// Convert method
        /// </summary>
        /// <param name="input"></param>
        /// <returns>ConversionResult</returns>
        private ConversionResult Convert(ConversionInput input)
        {
            var conversionFactor = _dataContext.UnitConversionFactors
              .Where(p => p.SourceUnit.ToLower() == input.SourceUnit.ToLower() &&
              p.ConversionUnit.ToLower() == input.ConversionUnit.ToLower()).FirstOrDefault();

            if (conversionFactor == null)
            {
                return null;
            }

            ConversionResult result = new ConversionResult
            {
                SourceUnit = input.SourceUnit,
                InputValue = input.InputValue,
                ConversionUnit = input.ConversionUnit
            };

            if (conversionFactor.ConversionFactor == -1)
            {

                if (input.SourceUnit.ToLower() == "fahrenheit")
                {
                    result.ConvertedValue = (input.InputValue - 32) * 0.5556;
                }

                if (input.SourceUnit.ToLower() == "celsius")
                {
                    result.ConvertedValue = (input.InputValue * 1.8) + 32;
                }
            }
            else
            {
                result.ConvertedValue = input.InputValue * conversionFactor.ConversionFactor.Value;
            }

            return result;
        }
    }
}
