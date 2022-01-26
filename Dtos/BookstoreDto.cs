namespace Dtos;

[System.Serializable]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true)]
[System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
public class BookstoreDto
{

    private BookstoreBook[] _bookField;

    [System.Xml.Serialization.XmlElement("book")]
    public BookstoreBook[] Book
    {
        get => _bookField;
        set => _bookField = value;
    }
}

[System.Serializable]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true)]
public class BookstoreBook
{

    private BookstoreBookTitle _titleField;

    private string[] _authorField;

    private ushort _yearField;

    private decimal _priceField;

    private string _categoryField;

    private string _coverField;

    public BookstoreBookTitle Title
    {
        get => _titleField;
        set => _titleField = value;
    }

    [System.Xml.Serialization.XmlElement("author")]
    public string[] Author
    {
        get => _authorField;
        set => _authorField = value;
    }

    public ushort Year
    {
        get => _yearField;
        set => _yearField = value;
    }

    public decimal Price
    {
        get => _priceField;
        set => _priceField = value;
    }

    [System.Xml.Serialization.XmlAttribute]
    public string Category
    {
        get => _categoryField;
        set => _categoryField = value;
    }

    [System.Xml.Serialization.XmlAttribute]
    public string Cover
    {
        get => _coverField;
        set => _coverField = value;
    }
}

[System.Serializable]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true)]
public class BookstoreBookTitle
{

    private string _langField;

    private string _valueField;

    [System.Xml.Serialization.XmlAttribute]
    public string Lang
    {
        get => _langField;
        set => _langField = value;
    }

    [System.Xml.Serialization.XmlText]
    public string Value
    {
        get => _valueField;
        set => _valueField = value;
    }
}