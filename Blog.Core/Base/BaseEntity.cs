namespace Blog.Core.Base
{
	public abstract class BaseEntity : IBaseEntity
	{ 
        public virtual Guid Id { get; set; } = Guid.NewGuid();
        public virtual Guid? CreatedBy { get; set; }
        public virtual Guid? ChangedBy { get; set; }

        public virtual DateTime? CreatedTime { get; set; }
        public virtual DateTime? ChangedTime { get; set; }

        public virtual bool IsActive { get; set; } = false;
    }
}

