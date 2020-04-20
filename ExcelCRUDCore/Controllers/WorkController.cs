using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BusinessServiceLayer.Implementations;
using DataLayer.Entityes;
using ExcelCRUDCore.Helpers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExcelCRUDCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        private readonly WorkImp workImp;
        public WorkController(WorkImp workImp)
        {
            this.workImp = workImp;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/dd-MM-yyyy
        [HttpGet("{date}")]
        /// <summary>
        /// Возвращаем записи по дате создания
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<ImportResponse<IEnumerable<UserWorkInfo>>> Get(string date)
        {
            var workInfoList = new List<UserWorkInfo>();
            try
            {
                var showByDate = GetDate(date);
                return ImportResponse<IEnumerable<UserWorkInfo>>.GetResult(0, "OK", await workImp.GetAllInfoByDate(showByDate));

            }
            catch (ValidationException e)
            {
                return ImportResponse<IEnumerable<UserWorkInfo>>.GetResult(-1, e.Message, workInfoList);
                throw;
            }
            catch (Exception e)
            {
                return ImportResponse<IEnumerable<UserWorkInfo>>.GetResult(-1, e.StackTrace, workInfoList);
                throw;
            }
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>
        [HttpPut]
        /// <summary>
        /// Обновление сущности UserWorkInfo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="workInfo"></param>
        /// <returns></returns>
        public async Task<ImportResponse<UserWorkInfo>> Put(int id, [FromBody]UserWorkInfo workInfo)
        {
            if (workInfo == null)
            {
                return ImportResponse<UserWorkInfo>.GetResult(-1, "WorkInfo is null", workInfo);
            }
            else
            {
                try
                {
                    workInfo.ChangeDate = DateTime.UtcNow;
                    await workImp.Update(workInfo);
                    return ImportResponse<UserWorkInfo>.GetResult(0, "OK", workInfo);
                }
                catch (KeyNotFoundException e)
                {
                    return ImportResponse<UserWorkInfo>.GetResult(-1, "Информация о работе не найдена" + e.Message, workInfo);
                    throw;
                }
                catch (Exception e)
                {
                    return ImportResponse<UserWorkInfo>.GetResult(-1, e.StackTrace, workInfo);
                }
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        /// <summary>
        /// Удаление сущности UserWorkInfo по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ImportResponse<int>> Delete(int id)
        {
            try
            {
                await workImp.Remove(id);
                return ImportResponse<int>.GetResult(0, "OK", id);
            }
            catch (KeyNotFoundException e)
            {
                return ImportResponse<int>.GetResult(-1, "Информация о работе не найдена" + e.Message, id);
                throw;
            }
            catch (Exception e)
            {
                return ImportResponse<int>.GetResult(-1, e.StackTrace, id);
            }
        }

        /// <summary>
        /// Парсим дату
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected DateTime GetDate(string value)
        {
            DateTime result = default(DateTime);
            if (!Formatters.TryStringToDate(value, out result))
            {
                throw new ValidationException(string.Format("Ошибка при парсинге даты"));
            }

            return result;
        }
    }
}
