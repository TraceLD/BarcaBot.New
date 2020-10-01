<script>
	export let segment;

	let menu;

	function onBurgerClick() {
		if (menu.classList.contains("hidden")) {
			menu.classList.remove("hidden");
		} else {
			menu.classList.add("hidden");
		}
	}
</script>

<nav class="flex items-center justify-between flex-wrap py-6
	  px-4 lg:px-8">
	<a
		aria-current={segment === undefined ? 'page' : undefined}
		href="."
		class="flex items-center flex-shrink-0">
		<span class="font-semibold text-xl tracking-tight">BarcaBot</span>
	</a>
	<div class="block lg:hidden">
		<button
			id="burger"
			on:click={onBurgerClick}
			class="flex items-center px-3 py-2 rounded hover:text-gray-500
		  duration-300">
			<svg
				class="fill-current h-3 w-3"
				viewBox="0 0 20 20"
				xmlns="http://www.w3.org/2000/svg">
				<title>Menu</title>
				<path d="M0 3h20v2H0V3zm0 6h20v2H0V9zm0 6h20v2H0v-2z" />
			</svg>
		</button>
	</div>
	<div
		bind:this={menu}
		id="menu"
		class="w-full hidden lg:flex lg:items-center lg:w-auto">
		<div>
			<a
				href="https://discord.com/oauth2/authorize?client_id=554961196764233735&permissions=117760&scope=bot"
				class="block mt-4 lg:inline-block lg:mt-0 hover:text-gray-500 mr-4
		  duration-300">
				Invite
			</a>
			<a
				aria-current={segment === 'commands' ? 'page' : undefined}
				href="commands"
				class="block mt-4 lg:inline-block lg:mt-0 hover:text-gray-500 mr-4
			duration-300">
				Commands
			</a>
			<a
				rel="prefetch"
				href="https://github.com/TraceLD/BarcaBot"
				class="block mt-4 lg:inline-block lg:mt-0 hover:text-gray-500 mr-4
			duration-300">
				GitHub
			</a>
		</div>
	</div>
</nav>
