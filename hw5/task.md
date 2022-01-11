# Подсчёт слов с Apache Spark RDD

## Подключение на кластер

Для подключения на кластер вам потребуется приложение терминал и ssh подключение.
На операционных системах семейства windows рекомендую:

- [MobaXterm] - мощная функциональность scp и терминала в одном приложении, portable версия, юзерфрендли, красиво)
- [WinSCP] - Подходит для копирования файлов, может работать и с терминалом.
- [PuTTY] - Два разных приложения для терминала (putty.exe) и для копирования файлов (pscp.exe)

Если у вас \*nix подобная ОС рекомендовать ничего не стану, наверняка есть свои предпочтения.
Подключение будет происходить примерно так, пароль сообщу отдельно в Telegram-чат:

```sh
ssh sshuser@CLUSTER-ssh.azurehdinsight.net
```

## Копирование файлов на кластер

Копировать файлы на кластер нет необходимости. Исходные файлы будут на hdfs:

- /example/data/gutenberg/davinci.txt
- /example/data/gutenberg/davinci1M.txt
- /example/data/gutenberg/davinci100M.txt
- /example/data/gutenberg/davinci1G.txt
- /example/data/gutenberg/davinci4G.txt

## Запуск приложения

Вам требуется написать приложение на python или scala для запуска через spark-submit. см. [пример](https://pythonexamples.org/pyspark-word-count-example/)

## Время работы

Для оценки времени работы приложения воспользуйтесь логами yarn

```sh
yarn logs --applicationId $appId > my.log
```

## Результаты

Сравните как работают ваши приложения из 2, 3 и 5 работ. Сделайте выводы

[mobaxterm]: https://mobaxterm.mobatek.net/
[winscp]: https://winscp.net/eng/download.php
[putty]: https://www.chiark.greenend.org.uk/~sgtatham/putty/latest.html
