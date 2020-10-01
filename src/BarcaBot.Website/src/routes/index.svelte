<svelte:head>
	<title>BarcaBot - FC Barcelona themed bot</title>
</svelte:head>

<script>
	import Button from "../components/Button.svelte";
</script>

<div class="mt-40">
	<h1 class="text-center text-6xl font-bold">BarcaBot</h1>

	<p class="text-center text-xl">FC Barcelona themed Discord bot</p>
	<p class="text-center text-xl mb-4">Player statistics, matches schedule and more</p>

	<div class="flex justify-center">
		<Button link="https://discord.com/oauth2/authorize?client_id=554961196764233735&permissions=117760&scope=bot" margin="mr-2" text="Invite" />
		<Button link="/commands" margin="mr-2" text="Commands" />
		<Button link="https://github.com/TraceLD/BarcaBot" margin="mr-2" text="Source code" />
	</div>
</div>
