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

db.createCollection("leaguescorers");
db.createCollection("leaguetable");
db.createCollection("players");
db.createCollection("scheduledmatches");
db.createCollection("scorers");