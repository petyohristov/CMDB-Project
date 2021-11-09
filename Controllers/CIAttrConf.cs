using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMDB_API_services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CIAttrConf : ControllerBase
    {
        private readonly IConfiguration Configuration;

        public CIAttrConf(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // GET: api/<CIAttrConf>
        [HttpGet]
        public string Get()
        {

            string con = Configuration["ConnectionStrings:CMDBConnString"];
            string retJson = "{}";

            using (var conn = new SqlConnection(con))
            {
                using (var cmd = new SqlCommand("SELECT CITYPE_ID as Attribute_ID, CITYPE_NAME as Name, 'true' as Active, 'true' as Mandatory, 'false' as Multiple, 'BaseCI' as [Type], " +
                            "CITYPE_DESCRIPTION as Info FROM CITYPE WHERE ( CITYPE_TYPE = 'MAIN' AND TENANT = 'CMDB' ) for json path", conn))
                {
                    cmd.CommandTimeout = 30;

                    conn.Open();

                    string getValue = cmd.ExecuteScalar().ToString();
                    if (getValue != null)
                    {
                        retJson = getValue.ToString();
                    }
                    conn.Close();
                    return retJson;
                }
            }


        }

        // GET api/<CIAttrConf>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CIAttrConf>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CIAttrConf>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CIAttrConf>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
