<script>
	import Nav from '../components/Nav.svelte';
	import Footer from '../components/Footer.svelte';

	export let segment;
</script>

<div class="flex flex-col min-h-screen w-full text-gray-200 bg-gray-800">
	<div class="mb-8">
		<Nav {segment}/>
	</div>
	
	<main>
		<slot></slot>
	</main>

	<Footer />
</div>