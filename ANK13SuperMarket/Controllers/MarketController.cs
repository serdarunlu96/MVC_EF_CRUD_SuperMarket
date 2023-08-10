using ANK13SuperMarket.Context;
using ANK13SuperMarket.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ANK13SuperMarket.Controllers
{
    public class MarketController : Controller
    {
        private readonly MarketDbContext _db;



        public MarketController(MarketDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()  //Index, stokta olanları getirir !
        {
            //Bütün ürünlerin listelendiği yer
            //Bütün ürünleri getir ve görünüme gönder.

            ViewBag.StockDurumu = false;
            return View(_db.Products.Where(u => u.Stock).ToList());

        }

        public IActionResult StoktaOlmayanlariGetir()  //stokta olmayanları getirir !
        {
            //Bütün ürünlerin listelendiği yer
            //Bütün ürünleri getir ve görünüme gönder.
            ViewBag.StockDurumu = true;
            return View("Index", _db.Products.Where(u => !u.Stock).ToList());

        }

        [HttpGet]
        public IActionResult Ekle()
        {
            //Bu action ekleme sayfasının açılması için gerekli.
            //O yüzden GET
            //Ekleme formunun görünmesi için
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(Product urun)
        {
            //Bu action ekleme işlemi için gerekli o yüzden POST
            //DB'ye ekle

            _db.Products.Add(urun);
            _db.SaveChanges();

            //Ana listeye git
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            return View(_db.Products.Find(id));
        }

        [HttpPost]
        public IActionResult Guncelle(Product urun)
        {
            //Bu action ekleme işlemi için gerekli o yüzden POST
            //DB'ye ekle

            _db.Products.Update(urun);
            _db.SaveChanges();

            //Ana listeye git
            return RedirectToAction("Index");
        }


        public IActionResult Sil(int id)
        {
            //Önce gelen id ye ait olan ürünü bul.
            //Sonra da onu silme view'una aktar.

            return View(_db.Products.Find(id));

        }

        [HttpPost]
        public IActionResult Sil(Product urun)
        {
            //şimdi bu ürünü sil
            _db.Products.Remove(urun);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
