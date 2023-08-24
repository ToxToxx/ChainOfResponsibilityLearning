using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibilityLearning
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Merchant merchant = new Merchant(false, true, true);

            GoldPaymentHandler goldPaymentHandler = new GoldPaymentHandler();
            CrystalPaymentHandler crystalPaymentHandler = new CrystalPaymentHandler();
            ResourcesPaymentHandler resourcesPaymentHandler = new ResourcesPaymentHandler();

            goldPaymentHandler.Merchant = crystalPaymentHandler;
            crystalPaymentHandler.Merchant = resourcesPaymentHandler;

            goldPaymentHandler.Handle(merchant);

            Console.ReadKey();
        }
    }
    class Merchant
    {
        // gold payment
        public bool GoldTransfer { get; set; }
        // crystal payment
        public bool CrystalTransfer { get; set; }
        // resource payment
        public bool ResourceTransfer { get; set; }
        public Merchant(bool gt, bool ct, bool rt)
        {
            GoldTransfer = gt;
            CrystalTransfer = ct;
            ResourceTransfer = rt;
        }
    }

    abstract class MerchantHandler
    {
        public MerchantHandler Merchant { get; set; }
        public abstract void Handle(Merchant merchant);
    }

    //Concrete handler
    class GoldPaymentHandler : MerchantHandler
    {
        public override void Handle(Merchant merchant)
        {
            if (merchant.GoldTransfer == true)
                Console.WriteLine("This merchant accepts gold payment");
            else if (Merchant != null)
                Merchant.Handle(merchant);
        }
    }

    //Concrete handler 2
    class CrystalPaymentHandler : MerchantHandler
    {
        public override void Handle(Merchant merchant)
        {
            if (merchant.ResourceTransfer == true)
                Console.WriteLine("This merchant accepts crystals payment");
            else if (Merchant != null)
                Merchant.Handle(merchant);
        }
    }

    //Concrete handler 3
    class ResourcesPaymentHandler : MerchantHandler
    {
        public override void Handle(Merchant merchant)
        {
            if (merchant.CrystalTransfer == true)
                Console.WriteLine("This merchant accepts resources payment");
            else if (Merchant != null)
                Merchant.Handle(merchant);
        }
    }
}
