using System.Security.Cryptography;

namespace HomeWork_HashTable;

public class myHashTable
{
    private Dictionary<int, List<string>> _myHash = null; //Пустая хэш-таблица
    private int _hashKey; //хэш-ключ
    private int _maxSize = 50; //Максимальное количество элементов в хэш-таблице
    private int offset = 5; //Переменная для прибавления к хэш-ключу при коллизии
    
    //Инициализация словаря-хэш-таблицы
    public myHashTable()
    {
        _myHash = new Dictionary<int, List<string>>(_maxSize);
    }
    
    //Метод для добавления записей в хэш-таблицу
    //При коллизии к старой хэш-функции прибавляется offset, пока не будет найдена свободная ячейка (не получается проверить)
    public void AddItem(string key)
    {
        List<string> item = new List<string>();
        //Значения в списке в записи хэш-таблицы значат:
        //0 - ключ-значение ячейки;
        //1 - удалён (1)/не удалён (0) элемент;
        int deletedValue = 2; //флаг для обозначения, что объект в хэш-таблице был удалён;
                              //1 - значение удалено; 0 - значение не удалено; 2 - такого хэш-ключа не было
        _hashKey = key.GetHashCode();
        if (_myHash.ContainsKey(_hashKey))
        {
            List<string> itemTemp = _myHash[_hashKey];
            if (itemTemp[1] == "1")
            {
                deletedValue = 1;
            }
            else
            {
                _hashKey = _hashKey + offset; //вычисляем второй хэш-ключ
                deletedValue = 0;
            }
        }
        item.Add(key);
        item.Add("0");
        if (deletedValue == 2)
        {
            _myHash.Add(_hashKey, item);
        }
        else if (deletedValue == 1)
        {
            _myHash[_hashKey] = item;
        }
        else
        {
            _myHash.Add(_hashKey, item);
        }
    }

    //Не удаляем элемент, как таковой, но помечаем его флажком удаления
    //Элемент остаётся в списке, но потом, если у нового элемента тот же хэш, мы ставим его на место этого элемента
    //Как понять, какой именно элемент удалять, если есть два одинаковых элемента по одному и тому же ключу, но один с оффсет, а другой без?
    public void DeleteItem(string key)
    {
        int hashKey = key.GetHashCode();
        if (_myHash[hashKey][0] != key)
        {
            hashKey = hashKey + offset;
        }
        _myHash[hashKey][1] = "1";
    }

    
    public void SearchItem(string key)
    {
        int hashKey = key.GetHashCode();
        string res = "Hash-Code Key-Value del-Flag \n";
        if (_myHash[hashKey][0] != key)
        {
            hashKey = hashKey + offset;
        }
        res += hashKey + "; ";
        for (int i = 0; i < _myHash[hashKey].Count; i++)
        {
            res += _myHash[hashKey][i] + "; ";
        }
        Console.WriteLine(res);
    }

    public void GetItems()
    {
        string res = "Hash-Code Key-Value del-Flag \n";
        foreach (var hash in _myHash)
        {
            res += hash.Key + "; ";
            for (int i = 0; i < _myHash[hash.Key].Count(); i++)
            {
                res += _myHash[hash.Key][i] + "; ";
            }
            res += "\n";
        }
        Console.WriteLine(res);
    }
}