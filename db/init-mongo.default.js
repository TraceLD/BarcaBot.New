db.createUser(
    {
        user: "",
        pwd: "",
        roles: [
            {
                role: "readWrite",
                db: "barcabot"
            }
        ]
    }
);

db.createCollection("players");
db.createCollection("matches");
db.createCollection("leaguetable");
db.createCollection("scorers");