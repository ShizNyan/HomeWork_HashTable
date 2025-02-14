using System.Drawing;
using System.Security.Cryptography;

namespace HomeWork_HashTable;

public unsafe class myHashTable
{
    private int _hashKey; //хэш-ключ
    private int _maxSize = 50; //Максимальное количество элементов в хэш-таблице
    private int _maxListSize = 10; //Максимальное количество элементов в списке хэш-таблицы по индексу
    private int _offset = 5; //Переменная для прибавления к хэш-ключу при коллизии
    private string*[] _myHash; //Массив для хранения указателей на списки с элементами
    string[,] items; //Список для списка элементов

    private string item; //Список для элементов
                                            //Значения в списке в записи хэш-таблицы значат:
                                            //0 - ключ-значение ячейки;
                                            //1 - удалён (1)/не удалён (0) элемент; (пока не реализовано и убрано)
    
    //Инициализация массива указателей на списки с элементами
    public myHashTable()
    {
        _myHash = new string*[_maxSize];
        items = new string[_maxSize, _maxListSize];
    }
    
    //Метод для добавления записей в хэш-таблицу
    //При коллизии к старой хэш-функции прибавляется offset, пока не будет найдена свободная ячейка (не получается проверить)
    public void AddItem(string key)
    {
        int deletedValue = 2; //флаг для обозначения, что объект в хэш-таблице был удалён;
                              //2 - такого хэш-ключа не было; 1 - значение удалено; 0 - значение не удалено
        _hashKey = key.Length % _maxSize; //вычисляем хэш
        if (_myHash[_hashKey] != null)
        {
            if (items[_hashKey, 0] != null)
            {
                deletedValue = 0;
            }
            
        }
        
        if (deletedValue == 2) //добавляем новый элемент в хэш-таблицу
        {
            items[_hashKey,0] = _hashKey.ToString();
            items[_hashKey, 1] = key; // + " 0";
            fixed (string* link = &items[_hashKey,0])
            {
                _myHash[_hashKey] = link;
            }; 
        }
        else if (deletedValue == 1)
        {
            //_myHash[_hashKey] = item;
            //тут заменяем помеченное флагом удаления значение
            //когда-нибудь
        }
        else //тут добавляем новый элемент в список по уже существующему хэш-ключу
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[_hashKey, i] == null)
                {
                    items[_hashKey, i] = key; // + " 0";
                    break;
                }
            }
        }
    }
    
    //Очищаем все элементы по определённому хэшу
    public void CleanHash(string key)
    {
        int hashKey = key.Length % _maxSize;
        if (_myHash[hashKey] != null)
        {
            for (int i = 0; i < _maxListSize-1; i++)
            {
                items[hashKey, i] = null;
            }
            _myHash[hashKey] = null;
        }
    }
    
    //Удаление определённого элемента по хэшу
    public void DeleteItem(string key)
    {
        int hashKey = key.Length % _maxSize;
        if (_myHash[hashKey] != null)
        {
            for (int i = 0; i < _maxListSize-1; i++)
            {
                if (key == items[hashKey, i])
                {
                    items[hashKey, i] = null;
                }
            }
        }
    }

    
    public void SearchItem(string key)
    {
        int hashKey = key.Length % _maxSize;
        string res = "";
        int hit = 0;
        if (_myHash[hashKey] != null)
        {
            for (int i = 0; i < _maxListSize-1; i++)
            {
                Console.WriteLine(items[hashKey, i]);
                if (items[hashKey, i] == key)
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

    public void GetItems()
    {
        string res = "Hash-Code Key-Value\n";
        
        for (int i = 0; i < _maxSize-1; i++)
        {
            if (items[i, 0] != null)
            {
                for (int j = 0; j < _maxListSize-1; j++)
                {
                    if (items[i,j] != null)
                    {
                        res += items[i, j] + "; ";
                    }
                }
                res += "\n";
            }
        }
        Console.WriteLine(res);
    }
}