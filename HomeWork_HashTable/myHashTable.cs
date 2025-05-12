namespace HomeWork_HashTable;

public class MyHashTable
{
    private readonly int _maxSize = 50; //Максимальное количество элементов в хэш-таблице
    private readonly int _maxListSize = 10; //Максимальное количество элементов в списке по индексу
    private readonly string?[,] _myHash; //Массив ключей (индексов хэш-таблицы) и значений (элементов хэш-таблицы)
    
    //private string[,] _items; //Список для списка элементов
    //private string _item; //Элемент хэш-таблицы
                            //Значения в списке в записи хэш-таблицы значат:
                            //0 - ключ-значение ячейки;
                            //1 - удалён (1)/не удалён (0) элемент; (пока не реализовано и убрано)
    
    //Инициализация массива указателей на списки с элементами
    public MyHashTable()
    {
        _myHash = new string[_maxSize, _maxListSize];
    }
    
    //Метод для добавления записей в хэш-таблицу
    //При коллизии в список по индексу добавляется новое значение
    public void AddItem(string key)
    {
        int hashKey = GetHashCode(key, _maxSize); //вычисляем хэш
        if (_myHash[hashKey, 0] == null) //проверяем, существует ли какое-либо значение по полученному индексу
        {
            _myHash[hashKey,0] = hashKey.ToString();
            _myHash[hashKey, 1] = key;
        }
        else //тут добавляем новый элемент в список по уже существующему индексу
        {
            for (int i = 1; i < _myHash.Length; i++)
            {
                if (_myHash[hashKey, i] == null)
                {
                    _myHash[hashKey, i] = key;
                    break;
                }
            }
        }
    }
    
    //Очищаем все элементы по определённому хэшу
    public void CleanHash(string key)
    {
        int hashKey = GetHashCode(key, _maxSize);
        if (_myHash[hashKey, 0] != null)
        {
            for (int i = 0; i < _maxListSize-1; i++)
            {
                _myHash[hashKey, i] = null;
            }
        }
    }
    
    //Удаление определённого элемента по хэшу
    public void DeleteItem(string key)
    {
        int hashKey = GetHashCode(key, _maxSize);
        if (_myHash[hashKey, 0] != null)
        {
            for (int i = 0; i < _maxListSize-1; i++)
            {
                if (key == _myHash[hashKey, i])
                {
                    _myHash[hashKey, i] = null;
                }
            }
        }
    }

    //Поиск элемента по хэшу
    public void SearchItem(string key)
    {
        int hashKey = GetHashCode(key, _maxSize);
        string res = "";
        int hit = 0;
        if (_myHash[hashKey, 0] != null)
        {
            for (int i = 0; i < _maxListSize-1; i++)
            {
                if (_myHash[hashKey, i] == key)
                {
                    res += "Item has been found\nHash-Code Key-Value\n";
                    res += hashKey.ToString() + " " + key;
                    hit++;
                }
            }
            if (hit == 0)
            {
                res = "No item " + key + " has been found";
            }
        }
        else
        {
            res = "No item " + key + " has been found";
        }
        Console.WriteLine(res);
    }

    //Вывод всех элементов хэш-таблицы 
    public void GetItems()
    {
        string res = "Hash-Code Key-Value\n";
        
        for (int i = 0; i < _maxSize-1; i++)
        {
            if (_myHash[i, 0] != null)
            {
                for (int j = 0; j < _maxListSize-1; j++)
                {
                    if (_myHash[i,j] != null)
                    {
                        res += _myHash[i, j] + "; ";
                    }
                }
                res += "\n";
            }
        }
        Console.WriteLine(res);
    }

    int GetHashCode(string key, int size)
    {
        int hash = key.Length % size;
        return hash;
    }
}