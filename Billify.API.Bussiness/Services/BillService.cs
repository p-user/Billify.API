using AutoMapper;
using Billify.API.Common.Dtos;
using Billify.API.Common.Interfaces;
using Billify.API.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Billify.API.Bussiness.Services
{
    public class BillService : IBillService
    {
        private IMapper Mapper { get; }
        private IGenericRepository<Bill> BillRepository { get; }
        private IGenericRepository<Client> ClientRepository { get; }
        private IGenericRepository<Bill_Product> BillProductRepository { get; }
        public BillService(IMapper mapper, IGenericRepository<Bill> billRepository, IGenericRepository<Bill_Product> billProductRepository, IGenericRepository<Client> clientRepository)
        {
            Mapper=mapper;
            BillRepository=billRepository;
            BillProductRepository=billProductRepository;
            ClientRepository=clientRepository;  
        }
        public async Task<int> UpdateBillAsync(BillDto updated_obj)
        {
            var bill_entity = Mapper.Map<Bill>(updated_obj);
            var client = await ClientRepository.GetByIdAsync(updated_obj.Client.Id);
            var bill = await BillRepository.GetByIdAsync(updated_obj.Id);
            //var bill_prd = await BillProductRepository.GetByIdAsync(updated_obj.products);
            var entity = Mapper.Map<Bill>(updated_obj);
            entity.client = client;
            //entity.products = bill_prd;
            BillRepository.Update(entity);
            await BillRepository.SaveChangesAsync();
            return bill_entity.Id;
        }

        public async Task<int> CreateBillAsync(BillDto billcreate)
        {
            var bill_entity = Mapper.Map<Bill>(billcreate);
            var bill_product_entity = Mapper.Map<Bill_Product>(billcreate);
            await BillRepository.InsertAsync(bill_entity);
            await BillRepository.SaveChangesAsync();
            await BillProductRepository.InsertAsync(bill_product_entity);
            await BillProductRepository.SaveChangesAsync();
            return bill_entity.Id;
        }

        public async Task DeleteBillAsync(int Id)
        {
            var bill_entity = await BillRepository.GetByIdAsync(Id);
            BillRepository.Delete(bill_entity);
            await BillRepository.SaveChangesAsync();

        }

        public async Task<BillDto> GetBillAsync(int Id)
        {
            var bill_entity = await BillRepository.GetByIdAsync(Id, (bill) => bill.client, (bill) => bill);
            return Mapper.Map<BillDto>(bill_entity);
        }

        public async Task<List<BillDto>> GetBillsAsync()
        {
            var bill_entities=await BillRepository.GetAsync(null, null);
            return Mapper.Map<List<BillDto>>(bill_entities);
        }
    }
}
