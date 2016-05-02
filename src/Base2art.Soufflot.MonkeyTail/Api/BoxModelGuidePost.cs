namespace Base2art.MonkeyTail.Api
{
    using System;
    using System.Linq;

    public class BoxModelGuidePost : IValueContainerOut<int>, IComparable<BoxModelGuidePost>
    {
        private readonly int value;

        private readonly string name;

        public BoxModelGuidePost()
            : this(0, "Center")
        {
        }

        public BoxModelGuidePost(int value, string name)
        {
            this.name = name;
            this.value = value;
        }
        
        private static BoxModelGuidePost CenterBacking = new BoxModelGuidePost(0, "Center");
        public static BoxModelGuidePost Center
        {
            get { return CenterBacking; }
        }

        private static BoxModelGuidePost LeftBacking = new BoxModelGuidePost(1, "Left");
        public static BoxModelGuidePost Left
        {
            get { return LeftBacking; }
        }

        private static BoxModelGuidePost LeftLeftBacking = new BoxModelGuidePost(2, "LeftLeft");
        public static BoxModelGuidePost LeftLeft
        {
            get { return LeftLeftBacking; }
        }

        private static BoxModelGuidePost RightBacking = new BoxModelGuidePost(3, "Right");
        public static BoxModelGuidePost Right
        {
            get { return RightBacking; }
        }

        private static BoxModelGuidePost RightRightBacking = new BoxModelGuidePost(4, "RightRight");
        public static BoxModelGuidePost RightRight
        {
            get { return RightRightBacking; }
        }

        private static BoxModelGuidePost TopBacking = new BoxModelGuidePost(5, "Top");
        public static BoxModelGuidePost Top
        {
            get { return TopBacking; }
        }

        private static BoxModelGuidePost TopTopBacking = new BoxModelGuidePost(6, "TopTop");
        public static BoxModelGuidePost TopTop
        {
            get { return TopTopBacking; }
        }

        private static BoxModelGuidePost BottomBacking = new BoxModelGuidePost(7, "Bottom");
        public static BoxModelGuidePost Bottom
        {
            get { return BottomBacking; }
        }

        private static BoxModelGuidePost BottomBottomBacking = new BoxModelGuidePost(8, "BottomBottom");
        public static BoxModelGuidePost BottomBottom
        {
            get { return BottomBottomBacking; }
        }

        private static BoxModelGuidePost HeaderBacking = new BoxModelGuidePost(9, "Header");
        public static BoxModelGuidePost Header
        {
            get { return HeaderBacking; }
        }

        private static BoxModelGuidePost FooterBacking = new BoxModelGuidePost(10, "Footer");
        public static BoxModelGuidePost Footer
        {
            get { return FooterBacking; }
        }

        public int Value
        {
            get { return this.value; }
        }
        
        public static BoxModelGuidePost[] Values
        {
            get
            {
                return new BoxModelGuidePost[] {
                    CenterBacking, 
                    LeftBacking, 
                    LeftLeftBacking, 
                    RightBacking, 
                    RightRightBacking, 
                    TopBacking, 
                    TopTopBacking, 
                    BottomBacking, 
                    BottomBottomBacking, 
                    HeaderBacking, 
                    FooterBacking
                }; 
            }
        }

        public static explicit operator BoxModelGuidePost(int val)
        {
            return Values.FirstOrDefault(x => x.Value == val);
        }

        public static implicit operator int(BoxModelGuidePost d)
        {
            return d.Value;
        }
    
        public int CompareTo(BoxModelGuidePost other)
        {
            if (other == null)
            {
                return -1;
            }
            
            return this.Value.CompareTo(other.Value);
        }
    }
}
