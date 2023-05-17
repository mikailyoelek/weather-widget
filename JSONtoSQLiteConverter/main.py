import json
import sqlite3

# https://stackoverflow.com/questions/8811783/convert-json-to-sqlite-in-python-how-to-map-json-keys-to-database-columns-prop
# https://www.codeproject.com/Tips/4067936/Load-JSON-File-with-Array-of-Objects-to-SQLite3-On

db = sqlite3.connect("citylist.db")
with open("citylist.json", encoding='utf-8-sig') as json_file:
    json_data = json.loads(json_file.read())

# Get the list of the columns in the JSON file
    columns = []
    column = []
    for data in json_data:
        column = list(data.keys())
        for col in column:
            if col not in columns:
                columns.append(col)

# Values of the columns in the JSON file in right order
    value = []
    values = []
    for data in json_data:
        for i in columns:
            value.append(str(dict(data).get(i)))
        values.append(list(value))
        value.clear()

# generate and create and insert queries and apply it to the sqlite3 database
    #create_query = "create table if not exists cities ({0})".format(" text,".join(columns))
    create_query = "create table if not exists cities (id integer primary key, name text, state text, country text," \
                   "coord text)"
    insert_query = "insert into cities ({0}) values (?{1})".format(",".join(columns), ",?" * (len(columns)-1))
    cursor = db.cursor()
    cursor.execute(create_query)
    cursor.executemany(insert_query, values)
    values.clear()
    db.commit()
    cursor.close()
