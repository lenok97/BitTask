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
            var todoItem = await _context.CameraParamsList.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpPost]
        public async Task<ActionResult<CameraParams>> PostCameraParams(float distance, float cameraHeight)
        {
            CameraParams cameraParams = new CameraParams(distance, cameraHeight);

            // сохраняет результат расчёта в базу данных
            _context.CameraParamsList.Add(cameraParams);
            await _context.SaveChangesAsync();
            var res = new Dictionary<string, float>();
            res.Add("alpha", (float)Math.Round(cameraParams.Angle, 2));
            res.Add("B", (float)Math.Round(cameraParams.B, 2));
            // выводит расстояние B, см и угол вертикального наклона 
            return CreatedAtAction(nameof(GetCameraParams), new { id = cameraParams.Id },
                res);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CameraParams>>> GetCameraParamsList()
        {
            return await _context.CameraParamsList.ToListAsync();
        }
    }
}
