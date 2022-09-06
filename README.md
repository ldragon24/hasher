# hasher
Средство фиксации и контроля исходного состояния программного комплекса

Программа предназначена для сбора и хранения контрольных сумм файлов в указанных каталогах

Программа написана с использованием dotnet 3.5, что позволяет ей работать в системах MS Windows XP и выше.

Программа состоит из 4 файлов:
1.	HASHER.exe  файл программы
2.	adodb.dll  библиотека для связи с базой данных
3.	db.mdb  база данных программы
4.	Interop.JRO.dll  библиотека для сжатия базы данных


Программа не требует установки, работает из любого каталога. Для запуска необходимо выполнить исполняемый файл - HASHER.exe.

## Алгоритмы вычисления контрольной суммы

В программе используются пять алгоритмов вычисления контрольной суммы фалов:
1.	MD5
2.	CRC32
3.	SHA256
4.	SHA512
5.	SHA1

В программе имеется возможность фильтрации по типу файлов, «Меню\Игнорируемые типы файлов»

## Фиксация изменений в файлах

Повторное сканирование файлов происходит:
1.	При повторном запуске программы
2.	Вызывается вручную в пункте меню «Обновить данные»
При выявленных изменениях в листе измененные данные помечаются красным цветом
Отсутствующие (удаленные) файлы помечаются серым цветом.
Просмотреть измененные файлы можно нажав на надпись в строке состояния.

Так же в программе предусмотрена проверка данных по таймеру, проверка осуществляется каждые три часа (00, 03, 06, 09, 12, 15, 18, 21) с записью обнаруженных изменений в файл change.log, находящийся в каталоге программы.

>11.08.2022 11:45:10|Не найден файл: |D:\###\_TMP\df.xls| Записанная контрольная сумма: |8f08c58b87f9e9b0f289745fae105e9e
>11.08.2022 11:17:16|Обнаружены изменения в файле: |D:\###\_TMP\Doc1.docx| Записанная контрольная сумма: |c21a97b4318b4cc8bf71b97fd66d2c6a| не соответствует текущей: |8002eaffc3b70f7b79b32f2ad6f92219
>11.08.2022 11:17:16|Обнаружены изменения в файле: |D:\###\_TMP\emcs_index_sql.sql| Записанная контрольная сумма: |516f548e426d5dadec1fe9c75c26110e| не соответствует текущей: |b0c5ed2c56c5f1159884bb089c639171

Для этого необходимо не закрывать программу, а свернуть, она будет находится в системном трее.
При обнаружении изменений в трее появиться сообщение и будет произведена запись в файл, если запись в файле уже существует, то повторно запись делаться не будет. 
Если данные изменения произведены корректно (т.е. Вами), к примеру, это конфигурационный файл, либо файл мнемосхемы, измененные согласно регламента, то можно принять данные изменения вызвав контекстное меню.
После нажатия кнопки "Принять изменения" изменения будут записаны в базу, с изменением даты и времени.

## Поиск дубликатов
 В программе предусмотрен механизм поиска дубликатов файлов.
 
 
## Передача данных

В программе имеется возможность сохранения данных в формат CSV:
«Меню\Сохранить список в файл»
 
При нажатии на данный пункт меню появится диалоговое окно с предложением сохранить файл, выбираем путь сохранения и имя фала, нажимаем «Сохранить»
 
После выполнения функции сохранения в указанном месте появиться файл.

 
 



