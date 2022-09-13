var s = new Stack("a", "b", "c");
Console.WriteLine($"size = {s.Size}, Top = '{s.Top}'");
var deleted = s.Pop();
Console.WriteLine($"Извлек верхний элемент '{deleted}' Size = {s.Size}");
s.Add("d");
Console.WriteLine($"size = {s.Size}, Top = '{s.Top}'");
s.Pop();
s.Pop();
s.Pop();
Console.WriteLine($"size = {s.Size}, Top = {(s.Top == null ? "null" : s.Top)}");
s.Pop();

//доп задание 1
s.Merge(new Stack("1", "2", "3"));
Console.WriteLine($"size = {s.Size}, Top = '{s.Top}'");

s = new Stack("a", "b", "c");
s.Merge(new Stack("1", "2", "3"));
Console.WriteLine($"size = {s.Size}, Top = '{s.Top}'");

//доп задание 2
var s1 = Stack.Concat(new Stack("a", "b", "c"),
    new Stack("1", "2", "3"), 
    new Stack("А", "Б", "В"));
Console.WriteLine($"size = {s1.Size}, Top = '{s1.Top}'");

//доп задание 3
var s2 = new Stack.StackItem("a", "b", "c");
Console.WriteLine($"size = {s2.Size}, Top = '{s2.Top}'");
deleted = s2.Pop();
Console.WriteLine($"Извлек верхний элемент '{deleted}' Size = {s2.Size}");
s2.Add("d");
Console.WriteLine($"size = {s2.Size}, Top = '{s2.Top}'");
s2.Pop();
s2.Pop();
s2.Pop();
Console.WriteLine($"size = {s2.Size}, Top = {s2.Top ?? "null"}");
s2.Pop();
s2.Add("d");
Console.WriteLine($"size = {s2.Size}, Top = '{s2.Top}'");
s2.Pop();
Console.WriteLine($"size = {s2.Size}, Top = {s2.Top ?? "null"}");


public class Stack
{
    //лектор сказал, что поля всегда должны быть с модификатором доступа  private,
    //но сделать без public я, если честно, не знаю как, так как обращаюсь к нему из метода расширения,
    //который находится в другом статическом классе, соответственно, наследование тоже сделать не могу
    //public List<string> _list;
    
    //PS все-таки получилось сделать его приватным)
    private List<string> _list;

    public Stack (params string[] elements)
    {
        var list = new List<string>(elements);
        _list = list;
    }

    public void Add(string element)
    {
        _list.Add(element);
    }

    public string? Pop()
    {
        // if (_list.Count == 0)
        //     throw new InvalidCastException("Стек пустой");
        string? popElelement = null;
        try
        {
            popElelement = _list[^1];
            _list.RemoveAt(_list.Count - 1);
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Стэк пустой");
        }
        return popElelement;
    }
    
    public int Size
    {
        get => _list.Count;
        set => Size = _list.Count;
    }

    public string? Top
    {
        get
        {
            if (_list.Count == 0)
                return null;
            return _list[^1];
        }
    }
    
    //подсмотрел такой способ сделать поле _list приватным на последней лекции)
    public string this[int key]
    {
        get => _list[key];
    }
    
    //доп задание 2
    public static Stack Concat(params Stack[] stacks)
    {
        var s = new Stack();
        foreach (var j in stacks)
        {
            for (var i = j.Size - 1; i >=0; i--)
            {
                s.Add(j._list[i]);
            }
        }
        return s;
    }
    
    // доп задание 3
    public class Items
    {
        public Items? Prev { get; set; }
        public string Element { get; set; }

        public Items (Items? prev, string element)
        {
            Prev = prev;
            Element = element;
        }
    }
    
    public class StackItem
    {
        private string? _top;
        private int _count;
        private Items? _item;

        public StackItem(params string[] elements)
        {
            _top = elements[^1];
            _count = elements.Length;
            foreach (string i in elements)
                _item = new Items(_item, i);
        }

        public void Add(string? element)
        {
            _count++;
            _top = element;
            _item = new Items(_item, element);
        }

        public int Size => _count;
        public string? Top => _top;
        
        public string? Pop()
        {
            string? popElement = null;
            try
            {
                if (_item == null)
                    throw new ItemNullException("Стек пустой");
                popElement = _item?.Element;
                _count--;
                _item = _item.Prev;
                _top = _item?.Element;
            }
            catch (ItemNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return popElement;
        }
    }
}

public class ItemNullException : Exception
{
    public ItemNullException(string message) 
        : base(message) {}
}


//доп задание 1
public static class StackExtentions
{
    public static void Merge(this Stack s1, Stack s2)
    {
        for (var i = s2.Size - 1; i >= 0; i--)
        {
            s1.Add(s2[i]);
        }
    }
}
