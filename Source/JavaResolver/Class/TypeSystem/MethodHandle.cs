using System;
using JavaResolver.Class.Constants;

namespace JavaResolver.Class.TypeSystem
{
    public class MethodHandle
    {
        private readonly LazyValue<IMemberReference> _member;

        public MethodHandle(MethodReferenceKind kind, IMemberReference member)
        {
            Kind = kind;
            _member = new LazyValue<IMemberReference>(member);
        }
        
        internal MethodHandle(JavaClassImage image, MethodHandleInfo info)
        {
            Kind = info.ReferenceKind;
            switch (Kind)
            {
                case MethodReferenceKind.GetField:
                case MethodReferenceKind.GetStatic:
                case MethodReferenceKind.PutField:
                case MethodReferenceKind.PutStatic:
                    _member = new LazyValue<IMemberReference>(() => image.ResolveField(info.ReferenceIndex));
                    break;
                
                case MethodReferenceKind.InvokeVirtual:
                case MethodReferenceKind.InvokeStatic:
                case MethodReferenceKind.InvokeSpecial:
                case MethodReferenceKind.NewInvokeSpecial:
                    _member = new LazyValue<IMemberReference>(() => image.ResolveMethod(info.ReferenceIndex));
                    break;
                
                case MethodReferenceKind.InvokeInterface:
                    throw new NotImplementedException();
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public MethodReferenceKind Kind
        {
            get;
            set;
        }

        public IMemberReference Member
        {
            get => _member.Value;
            set => _member.Value = value;
        }

        public override string ToString()
        {
            return $"{Kind} {Member}";
        }
    }
}