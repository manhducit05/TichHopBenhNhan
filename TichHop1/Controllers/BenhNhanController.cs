using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TichHop1.Models;

namespace TichHop1.Controllers
{
    public class BenhNhanController : ApiController
    {
        QLBenhNhanEntities db = new QLBenhNhanEntities();
        [HttpGet]
        public List<BenhNhan> DanhSach()
        {
            return db.BenhNhans.ToList();
        }
        [HttpPut]
        public IHttpActionResult SuaBn(BenhNhan bn)
        {
            var x = db.BenhNhans.FirstOrDefault(z => z.MaBN == bn.MaBN);
            if (x == null)
            {
                return NotFound();
            }
            else
            {
                x.HoTen = bn.HoTen;
                x.GioiTinh = bn.GioiTinh;
                x.NoiSinh = bn.NoiSinh;
                x.VienPhi = bn.VienPhi;
                db.SaveChanges();
                return Ok(x);
            }
        }
        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            var x = db.BenhNhans.FirstOrDefault(z => z.MaBN == id);
            if (x == null)
            {
                return NotFound();
            }
            else
            {
                db.BenhNhans.Remove(x);
                db.SaveChanges();
                return Ok();
            }
        }
    }
}
