namespace JavaResolver.Class.TypeSystem.Collections
{
    public class FieldCollection : MemberCollection<FieldDefinition>
    {
        public FieldCollection(ClassDefinition owner)
            : base(owner)
        {
        }

        protected override void SetOwner(FieldDefinition member, ClassDefinition newOwner)
        {
            member.DeclaringClass = newOwner;
        }
    }
}