<script>
	export let status;
	export let error;

	const dev = process.env.NODE_ENV === 'development';
</script>

<svelte:head>
	<title>{status}</title>
</svelte:head>

<h1 class="text-6xl font-bold mt-20 text-center">{status}</h1>

<p class="text-center">{error.message}</p>

{#if dev && error.stack}
	<pre>{error.stack}</pre>
{/if}
