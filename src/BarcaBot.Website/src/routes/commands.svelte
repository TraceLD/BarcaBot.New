<script>
    import Command from "../components/Command.svelte";

    let commands = [
        {
            name: "player",
            about:
                "Shows information (including statistics) about a Barca player.",
            usages: ["player <player_name>"],
            examples: ["player Messi", "player jong"]
        },
        {
            name: "schedule",
            about:
                "Shows upcoming Barca matches.",
            usages: ["schedule"],
            examples: ["schedule"]
        },
        {
            name: "table",
            about:
                "Shows LaLiga table.",
            usages: ["table <table_segment>"],
            examples: ["table top", "table bottom"]
        },
        {
            name: "scorers",
            about:
                "Shows LaLiga's current top scorers.",
            usages: ["scorers"],
            examples: ["scorers"]
        },
        {
            name: "stats",
            about:
                " Shows statistics chart for a given player/players.",
            usages: ["stats <player_name>", "stats <player_name1> <player_name2>", "stats <player_name1> <player_name2> <player_name3> etc."],
            examples: ["stats messi", "stats messi fati", "stats messi fati coutinho"]
        }
    ];
</script>

<svelte:head>
    <title>BarcaBot Commands</title>
</svelte:head>
<h1 class="text-center text-3xl font-semibold">BarcaBot Commands</h1>

<div class="max-w-screen-lg mt-10 mx-auto px-4">
    {#each commands as command}
    <div class="mb-6">
        <Command commandInfo={command} />
    </div>
    {/each}
</div>
