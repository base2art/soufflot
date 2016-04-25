
import codecs, sys
import os


prop_template = """

        private static {klazz} {propname}Backing = new {klazz}({propvalue}, "{propname}");
        public static {klazz} {propname}
        {{
            get {{ return {propname}Backing; }}
        }}
"""


file_template = """namespace {ns}
{{
    using System;
    using System.Linq;

    public class {klazz} : IValueContainerOut<int>, IComparable<{klazz}>
    {{
        private readonly int value;

        private readonly string name;

        public {klazz}() : this(0, "{defaultname}")
        {{
        }}

        public {klazz}(int value, string name)
        {{
            this.name = name;
            this.value = value;
        }}
        {props}
        
        public int Value
        {{
            get {{ return this.value; }}
        }}
        
        public static BoxModelGuidePost[] Values
        {{
            get
            {{
                return new BoxModelGuidePost[]
                {{
                    {proplist}
                }}; 
            }}
        }}

        public static explicit operator {klazz}(int val)
        {{
            return Values.FirstOrDefault(x => x.Value == val);
        }}

        public static implicit operator int({klazz} d)
        {{
            return d.Value;
        }}
	
        public int CompareTo({klazz} other)
        {{
            if (other == null)
            {{
                return -1;
            }}
            
            return this.Value.CompareTo(other.Value);
        }}
    }}
}}
"""


class EnumDescription:
  def __init__(self, ns, klazz, items):
    self.ns = ns
    self.klazz = klazz
    self.items = items


def do_file(path, f_name):
  p = os.path.join(path, f_name)
  fh = open(p)
  lines = fh.read()
  fh.close()
  lines = ("!" + "!".join(lines.split("!")[1:])).split('\n')
  
  ns = ""
  klazz = ""
  values = []
  for line in lines:
    cl = line.strip()
    if cl.startswith("!"):
      ns = cl[1:]
      continue
      
    if cl.startswith(":"):
      klazz = cl[1:]
      continue
      
    if cl.isprintable() and (len(cl) > 0):
      values.append(cl)
  
  return EnumDescription(ns, klazz, values)
  

def create_text(enum):
  props = []
  prop_map = {}
  
  prop_map["klazz"] = enum.klazz
  prop_map["ns"] = enum.ns
  
  count = 0
  for item in enum.items:
    prop_map["propname"] = item
    prop_map["propvalue"] = count
  
    prop = prop_template.format_map(prop_map)

    props.append(prop)
    count += 1
    
  prop_map["props"] = "".join(props)
  prop_map["proplist"] = ", \n".join(map(lambda x: x + 'Backing', enum.items))
  prop_map["defaultname"] = enum.items[0]
  return file_template.format_map(prop_map)

path = "."
f_name = "BoxModelGuidePost.cs"
text = create_text(do_file(".", "BoxModelGuidePost.enum"))
p = os.path.join(path, f_name)
fh = open(p, "w")
fh.write(text)
fh.close()
