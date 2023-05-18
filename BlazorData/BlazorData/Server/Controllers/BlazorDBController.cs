using BlazorData.Server.Data;
using BlazorData.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace BlazorData.Server.Controllers
{
    [Authorize]
    [ApiController]
    public class BlazorDBController : ControllerBase
    {
        private readonly BlazorDBContext _context; 
        public BlazorDBController(BlazorDBContext context )
        {
            _context = context; 
        }

        [HttpGet]
        [Route("api/Payload/GetAsync")]
        public async Task<List<PayloadDBData>> GetAsync()
        {
            var result = await _context.PayloadData.AsNoTracking().ToListAsync();
            Console.WriteLine(result.Count);
            List<PayloadDBData> ColPayload = new List<PayloadDBData>(); 
            foreach (var item in result)
            {
                PayloadDBData objPayloadDTO = new PayloadDBData();
                objPayloadDTO.Id =
                    item.Id;
                objPayloadDTO.DeviceId =
                    item.DeviceId;
                objPayloadDTO.DeviceType =
                    item.DeviceType;
                objPayloadDTO.DeviceName =
                    item.DeviceName;
                objPayloadDTO.GroupId =
                    item.GroupId;
                objPayloadDTO.DataType =
                    item.DataType;
                objPayloadDTO.Timestamp =
                    item.Timestamp;
                objPayloadDTO.UserName =
                    item.UserName;
                ColPayload.Add(objPayloadDTO);
            }
            return ColPayload;
        }

        [HttpPost]
        [Route("api/Payload/Post")]
        public void Post([FromBody] PayloadDBData paramPayload)
        {
            Models.PayloadData objPayload = new Models.PayloadData();
              
            objPayload.Id =
                     paramPayload.Id;
            objPayload.DeviceId =
                paramPayload.DeviceId; 
            objPayload.DeviceType =
                paramPayload.DeviceType;
            objPayload.DeviceName =
                paramPayload.DeviceName;
            objPayload.GroupId =
                paramPayload.GroupId;
            objPayload.DataType =
                paramPayload.DataType;
            objPayload.Timestamp =
                paramPayload.Timestamp;
            //objPayload.SubData = paramPayload.SubData;
            _context.PayloadData.Add(objPayload);
            _context.SaveChanges();
        }

        [HttpPut]
        [Route("api/Payload/Put")]
        public void Put([FromBody] PayloadDBData objPayload)
        { 

            var ExistingPayload =_context.PayloadData.Where(x => x.Id == objPayload.Id).FirstOrDefault();
            if (ExistingPayload != null)
            {
                ExistingPayload.Id =
                   objPayload.Id;
                ExistingPayload.DeviceId =
                    objPayload.DeviceId;
                ExistingPayload.UserName =
                    objPayload.UserName;
                ExistingPayload.DeviceType =
                    objPayload.DeviceType;
                ExistingPayload.DeviceName =
                    objPayload.DeviceName;
                ExistingPayload.GroupId =
                    objPayload.GroupId;
                ExistingPayload.DataType =
                    objPayload.DataType;
                ExistingPayload.Timestamp =
                    objPayload.Timestamp;
                //ExistingPayload.SubData =  objPayload.SubData;
                _context.SaveChanges();
            }
        }

        [HttpDelete]
        [Route("api/Payload/Delete/{id}")]
        public void Delete(Guid id)
        {
            var ExistingPayload =  _context.PayloadData.Where(x => x.Id == id).FirstOrDefault();
            if (ExistingPayload != null)
            {
                _context.PayloadData.Remove(ExistingPayload);
                _context.SaveChanges();
            }
        }
    }
}