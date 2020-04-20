using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BusinessServiceLayer.Implementations;
using DataLayer.Entityes;
using ExcelCRUDCore.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExcelCRUDCore.Controllers
{
    /// <summary>
    /// Информация/изменение пользователей
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserImp userImp;
        private readonly WorkImp workImp;
        public UserController(UserImp userImp, WorkImp workImp)
        {
            this.userImp = userImp;
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
        public async Task<ImportResponse<IEnumerable<User>>> Get(string date)
        {
            var list = new List<User>();
            try
            {
                var showByDate = GetDate(date);
                return ImportResponse<IEnumerable<User>>.GetResult(0, "OK", await userImp.GetAllInfoByDate(showByDate));

            }
            catch (ValidationException e)
            {
                return ImportResponse<IEnumerable<User>>.GetResult(-1, e.Message, list);
                throw;
            }
            catch (Exception e)
            {
                return ImportResponse<IEnumerable<User>>.GetResult(-1, e.StackTrace, list);
                throw;
            }
        }

        // POST api/<controller>
        [HttpPost]
        /// <summary>
        /// Импортируем Excel файл и сохраняем в БД.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ImportResponse<List<User>>> ImportExcelFile(IFormFile file, CancellationToken cancellationToken)
        {
            if (file == null || file.Length <= 0)
            {
                return ImportResponse<List<User>>.GetResult(-1, "formfile is empty");
            }
            if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return ImportResponse<List<User>>.GetResult(-1, "Not Support file extension");
            }

            var list = new List<User>();
            var workInfoList = new List<UserWorkInfo>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream, cancellationToken);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    ExcelWorksheet worksheetWorkInfo = package.Workbook.Worksheets[1];
                    var rowCount = worksheet.Dimension.Rows;
                    var rowCount1 = worksheetWorkInfo.Dimension.Rows;

                    for (int row = 1; row <= rowCount; row++)
                    {
                        try
                        {
                            list.Add(new User
                            {
                                Name = worksheet.Cells[row, 1].Value.ToString().Trim(),
                                LastName = worksheet.Cells[row, 2].Value.ToString().Trim(),
                                Middlename = worksheet.Cells[row, 3].Value.ToString().Trim(),
                                Age = int.Parse(worksheet.Cells[row, 4].Value.ToString().Trim()),
                                Address = worksheet.Cells[row, 5].Value.ToString().Trim(),
                                City = worksheet.Cells[row, 6].Value.ToString().Trim(),
                                Country = worksheet.Cells[row, 7].Value.ToString().Trim(),
                                Region = worksheet.Cells[row, 8].Value.ToString().Trim(),
                                Phone = worksheet.Cells[row, 9].Value.ToString().Trim(),
                                CompanyName = worksheet.Cells[row, 10].Value.ToString().Trim(),
                                Email = worksheet.Cells[row, 11].Value.ToString().Trim(),
                                SecondEmail = worksheet.Cells[row, 12].Value.ToString().Trim(),
                                Eyes = worksheet.Cells[row, 13].Value.ToString().Trim(),
                                Hair = worksheet.Cells[row, 14].Value.ToString().Trim(),
                                Nose = worksheet.Cells[row, 15].Value.ToString().Trim(),
                                Head = worksheet.Cells[row, 16].Value.ToString().Trim(),
                                Tattoo = worksheet.Cells[row, 17].Value.ToString().Trim(),
                                BestSkills = worksheet.Cells[row, 18].Value.ToString().Trim(),
                                Car = worksheet.Cells[row, 19].Value.ToString().Trim(),
                                LoveAnimal = worksheet.Cells[row, 20].Value.ToString().Trim(),
                                CreateDate = DateTime.UtcNow
                            });
                        }
                        catch (NullReferenceException e)
                        {
                            return ImportResponse<List<User>>.GetResult(-1, "Ошибка загрузки файла, вероятно не заполнены столбцы." + e.Message, list);
                            throw;
                        }
                        catch (Exception e)
                        {
                            return ImportResponse<List<User>>.GetResult(-1, e.StackTrace, list);
                            throw;
                        }
                    }
                    foreach (var l in list)
                    {
                        await userImp.Add(l);
                    }
                    for (int row = 1; row <= rowCount1; row++)
                    {
                        try
                        {
                            workInfoList.Add(new UserWorkInfo
                            {
                                CompanyName = worksheetWorkInfo.Cells[row, 1].Value.ToString().Trim(),
                                Location = worksheetWorkInfo.Cells[row, 2].Value.ToString().Trim(),
                                Building = worksheetWorkInfo.Cells[row, 3].Value.ToString().Trim(),
                                Salary = int.Parse(worksheetWorkInfo.Cells[row, 4].Value.ToString().Trim()),
                                Address = worksheetWorkInfo.Cells[row, 5].Value.ToString().Trim(),
                                City = worksheetWorkInfo.Cells[row, 6].Value.ToString().Trim(),
                                Country = worksheetWorkInfo.Cells[row, 7].Value.ToString().Trim(),
                                Region = worksheetWorkInfo.Cells[row, 8].Value.ToString().Trim(),
                                Phone = worksheetWorkInfo.Cells[row, 9].Value.ToString().Trim(),
                                SecondPhone = worksheetWorkInfo.Cells[row, 10].Value.ToString().Trim(),
                                Email = worksheetWorkInfo.Cells[row, 11].Value.ToString().Trim(),
                                SecondEmail = worksheetWorkInfo.Cells[row, 12].Value.ToString().Trim(),
                                Rank = worksheetWorkInfo.Cells[row, 13].Value.ToString().Trim(),
                                Floor = worksheetWorkInfo.Cells[row, 14].Value.ToString().Trim(),
                                Colleagues = worksheetWorkInfo.Cells[row, 15].Value.ToString().Trim(),
                                KindOf = worksheetWorkInfo.Cells[row, 16].Value.ToString().Trim(),
                                TypeOfWork = worksheetWorkInfo.Cells[row, 17].Value.ToString().Trim(),
                                LCA = worksheetWorkInfo.Cells[row, 18].Value.ToString().Trim(),
                                NDA = worksheetWorkInfo.Cells[row, 19].Value.ToString().Trim(),
                                Parking = worksheetWorkInfo.Cells[row, 20].Value.ToString().Trim(),
                                CreateDate = DateTime.UtcNow
                            });
                        }
                        catch (NullReferenceException e)
                        {
                            return ImportResponse<List<User>>.GetResult(-1, "Ошибка загрузки файла, вероятно не заполнены столбцы." + e.Message, list);
                            throw;
                        }
                        catch (Exception e)
                        {
                            return ImportResponse<List<User>>.GetResult(-1, e.StackTrace, list);
                            throw;
                        }
                    }
                    foreach (var w in workInfoList)
                    {
                        await workImp.Add(w);
                    }
                }
                return ImportResponse<List<User>>.GetResult(0, "OK", list);
            }
        }

        // PUT api/<controller>
        [HttpPut]
        /// <summary>
        /// Обновление сущности User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<ImportResponse<User>> Put([FromBody]User user)
        {
            if (user == null)
            {
                return ImportResponse<User>.GetResult(-1, "User is null", user);
            }
            else
            {
                try
                {
                    user.ChangeDate = DateTime.UtcNow;
                    await userImp.Update(user);
                    return ImportResponse<User>.GetResult(0, "OK", user);
                }
                catch (KeyNotFoundException e)
                {
                    return ImportResponse<User>.GetResult(-1, "Пользователь не найден" + e.Message, user);
                    throw;
                }
                catch (Exception e)
                {
                    return ImportResponse<User>.GetResult(-1, e.StackTrace, user);
                }
            }
        }


        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        /// <summary>
        /// Удаление сущности User по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ImportResponse<int>> Delete(int id)
        {
            try
            {
                await userImp.Remove(id);
                return ImportResponse<int>.GetResult(0, "OK", id);
            }
            catch (KeyNotFoundException e)
            {
                return ImportResponse<int>.GetResult(-1, "Пользователь не найден" + e.Message, id);
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
