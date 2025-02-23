using System;
using Metalama.Patterns.Contracts;
namespace Doc.Invariants_Suspend
{
  public partial class Invoice
  {
    private decimal _amount;
    public decimal Amount
    {
      get
      {
        return this._amount;
      }
      set
      {
        try
        {
          this._amount = value;
          return;
        }
        finally
        {
          if (!this.AreInvariantsSuspended())
          {
            this.VerifyInvariants();
          }
        }
      }
    }
    private int _discountPercent;
    [Range(0, 100)]
    public int DiscountPercent
    {
      get
      {
        return this._discountPercent;
      }
      set
      {
        try
        {
          if (value < 0 || value > 100)
          {
            throw new ArgumentOutOfRangeException("The 'DiscountPercent' property must be between 0 and 100.", "value");
          }
          this._discountPercent = value;
          return;
        }
        finally
        {
          if (!this.AreInvariantsSuspended())
          {
            this.VerifyInvariants();
          }
        }
      }
    }
    private decimal _discountAmount;
    [Range(0, 100)]
    public decimal DiscountAmount
    {
      get
      {
        return this._discountAmount;
      }
      set
      {
        try
        {
          if (value < 0M || value > 100M)
          {
            throw new ArgumentOutOfRangeException("The 'DiscountAmount' property must be between 0 and 100.", "value");
          }
          this._discountAmount = value;
          return;
        }
        finally
        {
          if (!this.AreInvariantsSuspended())
          {
            this.VerifyInvariants();
          }
        }
      }
    }
    public virtual decimal DiscountedAmount => (this.Amount * (100 - this.Amount) / 100m) - this.DiscountAmount;
    [Invariant]
    private void CheckDiscounts()
    {
      if (this.DiscountedAmount < 0)
      {
        throw new PostconditionViolationException("The discounted amount cannot be negative.");
      }
    }
    [SuspendInvariants]
    public void UpdateDiscounts1(int percent, decimal amount)
    {
      using (this.SuspendInvariants())
      {
        try
        {
          this.DiscountAmount = amount;
          this.DiscountPercent = percent;
        }
        finally
        {
          if (!this.AreInvariantsSuspended())
          {
            this.VerifyInvariants();
          }
        }
        return;
      }
    }
    public void UpdateDiscounts2(int percent, decimal amount)
    {
      try
      {
        using (this.SuspendInvariants())
        {
          this.DiscountAmount = amount;
          this.DiscountPercent = percent;
        }
        return;
      }
      finally
      {
        if (!this.AreInvariantsSuspended())
        {
          this.VerifyInvariants();
        }
      }
    }
    private readonly InvariantSuspensionCounter _invariantSuspensionCounter = new();
    protected bool AreInvariantsSuspended()
    {
      return _invariantSuspensionCounter.AreInvariantsSuspended;
    }
    protected IDisposable SuspendInvariants()
    {
      this._invariantSuspensionCounter.Increment();
      return _invariantSuspensionCounter;
    }
    protected virtual void VerifyInvariants()
    {
      this.CheckDiscounts();
    }
  }
}