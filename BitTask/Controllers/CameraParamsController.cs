using BitTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CameraParamsController : ControllerBase
    {
        private readonly CameraParamsContext _context;

        public CameraParamsController(CameraParamsContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CameraParams>> GetCameraParams(long id)
        {
            var cameraParams = await _context.CameraParamsList.FindAsync(id);

            if (cameraParams == null)
            {
                return NotFound();
            }

            return cameraParams;
        }

        [HttpPost]
        public async Task<ActionResult<CameraParams>> PostCameraParams(float distance, float heightFromFloor)
        {
            CameraParams cameraParams = null;
            // возвращаем статус ошибки, если данные запроса не корректны
            try
            { 
            cameraParams = new CameraParams(distance, heightFromFloor);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }

            // сохраняет результат расчёта в базу данных 
            _context.CameraParamsList.Add(cameraParams);
            await _context.SaveChangesAsync();
            var result = new Dictionary<string, float>
            {
                { "alpha", (float)Math.Round(cameraParams.VerticalAngle, 2) },
                { "B", (float)Math.Round(cameraParams.HeightFromObject, 2) }
            };

            // выводит расстояние B и угол вертикального наклона 
            return CreatedAtAction(nameof(GetCameraParams), new { id = cameraParams.Id },
                result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CameraParams>>> GetCameraParamsList()
        {
            return await _context.CameraParamsList.ToListAsync();
        }
    }
}
