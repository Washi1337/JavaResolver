namespace JavaResolver.Class.TypeSystem.Collections
{
    public class MethodCollection : MemberCollection<MethodDefinition>
    {
        public MethodCollection(ClassDefinition owner)
            : base(owner)
        {
        }

        protected override void SetOwner(MethodDefinition member, ClassDefinition newOwner)
        {
            member.DeclaringClass = newOwner;
        }
    }
}