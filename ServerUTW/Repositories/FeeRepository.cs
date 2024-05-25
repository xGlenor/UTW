using BaseLibrary.Contracts;
using BaseLibrary.Models;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerUTW.Data;

namespace ServerUTW.Repositories;

public class FeeRepository : IFeeRepository
{
    private readonly AppDbContext _dbContext;

    public FeeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Fee> Delete(int feeID)
    {
        var fee = await _dbContext.Fees.FirstOrDefaultAsync(fee => fee.Id.Equals(feeID));

        if (fee == null)
            return null;

        _dbContext.Fees.Remove(fee);
        await _dbContext.SaveChangesAsync();
        return fee;
    }

    public async Task<List<Fee>> GetAll()
    {
        return await _dbContext.Fees.ToListAsync();
    }

    public async Task<Fee?> GetById(int feeID)
    {
        return await _dbContext.Fees.FindAsync(feeID);
    }

    public async Task<Fee> Insert(Fee fee)
    {
        await _dbContext.Fees.AddAsync(fee);
        await _dbContext.SaveChangesAsync();
        return fee;
    }


    public async Task<Fee> Update(int id, Fee fee)
    {
        var existingFee = await _dbContext.Fees.FirstOrDefaultAsync(fee => fee.Id.Equals(id));

        if (existingFee == null)
            return null;

        existingFee.IssueDate = fee.IssueDate;
        existingFee.PaymentDate = fee.PaymentDate;
        existingFee.Details = fee.Details;
        existingFee.isPaid = fee.isPaid;
        existingFee.Student = fee.Student;

        await _dbContext.SaveChangesAsync();
        return existingFee;
    }
}