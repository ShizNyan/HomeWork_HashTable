# HomeWork_HashTable
Личная (недо)реализация хэш-таблицы на C#.
Реализованы:
1. Инициализация хэш-таблицы при помощи массива string (int не получилось) ссылок и двумерного массива string;
2. добавление элементов по хэшированию (двойного хэширования нет. При коллизии элементы с одинаковым хэшем записываются в список под одним кодом);
3. удаление элемента (есть удаления точечного элемента и очищение всего списка вместе с хэшем);
4. поиск элемента по ключу;
5. вывод хэш-таблицы в консоль.

В планах:
1. Доработать рехэш;
2. сделать ресайз хэш-таблицы (увеличивать её, когда нет больше места в списке в двумерном массиве string).
